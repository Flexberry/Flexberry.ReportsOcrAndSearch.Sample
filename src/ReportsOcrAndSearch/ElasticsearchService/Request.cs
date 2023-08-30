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
        /// <param name="requestURL">URL запроса.</param>
        /// <param name="jsonData">Отправляемые данные в JSON-формате.</param>
        public static void SendPutRequest(string baseUrl, string requestURL, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                using (HttpResponseMessage response = client.PutAsync(new Uri(requestURL), httpContent).Result)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }
    }
}