namespace ElasticsearchService
{
    using System.Text;

    /// <summary>
    /// Запросы к Elastic.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Отправка Get-запроса.
        /// </summary>
        /// <param name="baseUrl">Адрес сервера.</param>
        /// <param name="requestURL">URL запроса.</param>
        /// <returns>Ответ от Elastic.</returns>
        public string SendGetRequest(string baseUrl, string requestURL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                using (HttpResponseMessage response = client.GetAsync(requestURL).Result)
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
        public string SendPutRequest(string baseUrl, string requestURL, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
                using (HttpResponseMessage response = client.PutAsync(requestURL, httpContent).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
        }
    }
}