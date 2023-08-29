namespace ElasticsearchService
{
    using System.Text;

    /// <summary>
    /// Запросы к Elastic.
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// Отправка Get-запроса.
        /// </summary>
        /// <param name="baseUrl">Адрес сервера.</param>
        /// <param name="requestURL">URL запроса.</param>
        /// <returns>Ответ от Elastic.</returns>
        public static string SendGetRequest(string baseUrl, string requestURL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                using (HttpResponseMessage response = client.GetAsync(new Uri(requestURL)).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
        }

        /// <summary>
        /// Отправка Put-запроса.
        /// </summary>
        /// <param name="baseUrl">Адрес сервера.</param>
        /// <param name="requestURL">URL запроса.</param>
        /// <param name="jsonData">Отправляемые данные в JSON-формате</param>
        /// <returns>Ответ от Elastic.</returns>
        public static string SendPutRequest(string baseUrl, string requestURL, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                using (HttpResponseMessage response = client.PutAsync(new Uri(requestURL), httpContent).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
        }
    }
}