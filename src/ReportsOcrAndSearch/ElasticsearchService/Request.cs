namespace ElasticsearchService
{
    using System.Text;

    /// <summary>
    /// Запросы к Elastic.
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// Отправка Put-запроса.
        /// </summary>
        /// <param name="baseUrl">Адрес сервера.</param>
        /// <param name="requestUrl">URL запроса.</param>
        /// <param name="jsonData">Отправляемые данные в JSON-формате.</param>
        public static void SendPutRequest(string baseUrl, string requestUrl, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri baseAddress = new Uri(baseUrl);
                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    using (HttpResponseMessage response = client.PutAsync(new Uri(baseAddress, requestUrl), httpContent).Result)
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
                catch (Exception ex)
                {
                    throw new HttpRequestException("Put request sended error!\n" + ex.Message);
                }           
            }
        }
    }
}