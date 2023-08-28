namespace IIS.ReportsOcrAndSearch.OdataBackend
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using IIS.ReportsOcrAndSearch.OdataBackend.RequestObjects;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Поиск документов в эластике.
    /// </summary>
    public class ElasticTools
    {
        private const string _indexName = "documents-index";
        private readonly IConfiguration config;

        /// <summary>
        /// Поиск документов в эластике.
        /// </summary>
        /// <param name="config">Конфигурация.</param>
        public ElasticTools(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Поиск документа.
        /// </summary>
        /// <param name="searchText">Текст для поиска.</param>
        /// <returns>Результаты поиска.</returns>
        public List<PdfSearchResult> SearchDocuments(string searchText)
        {
            var sendResultUrl = config["ElasticUrl"] + "/" + _indexName + "/_search";
            var buffer = Encoding.UTF8.GetBytes(GetJsonQuery(searchText));
            var resultList = new List<PdfSearchResult>();

            using (var byteContent = new ByteArrayContent(buffer))
            {
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                using var httpClient = new HttpClient();
                using var responseTask = httpClient.PostAsync(sendResultUrl, byteContent);

                responseTask.Wait();

                var res = responseTask.Result;

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using var readTask = responseTask.Result.Content.ReadAsStringAsync();

                    readTask.Wait();

                    var readResult = readTask.Result;
                    var json = JObject.Parse(readResult);
                    var hits = json.SelectToken("$.hits.hits")?.Children();

                    foreach (var hit in hits)
                    {
                        var fileInfo = hit.SelectToken("$._source.file");
                        var elem = new PdfSearchResult(
                            uploadKey: fileInfo.Value<string>("upload_key"),
                            fileName: fileInfo.Value<string>("name"),
                            uploadUrl: string.Empty,
                            pageNumber: fileInfo.Value<string>("pagenumber"),
                            totalPages: fileInfo.Value<string>("totalpages"));

                        resultList.Add(elem);
                    }
                }
            }

            return resultList;
        }

        /// <summary>
        /// Получить ограничение для эластика по тексту.
        /// </summary>
        /// <param name="searchText">Текст для поиска.</param>
        /// <returns>JSON для запроса.</returns>
        private static string GetJsonQuery(string searchText)
        {
            var jsonBody = new JObject(
                new JProperty(
                    "query",
                    new JObject(
                        new JProperty(
                            "match",
                            new JObject(
                                new JProperty("attachment.content", searchText))))),
                new JProperty("_source", "file"));

            return jsonBody.ToString();
        }
    }
}
