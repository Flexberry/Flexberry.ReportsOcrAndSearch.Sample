using Newtonsoft.Json;
using System.Text;


namespace ElasticsearchService
{
    /// <summary>
    /// Запросы к Elastic.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Настрой соединения с Elastic.
        /// </summary>
        private readonly ConnectionConfig _connectionConfig;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="connectionConfig">Настройки для отпрвки файлов в Elastic.</param>
        public Request(ConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
        }

        /// <summary>
        /// Отправка Get-запроса.
        /// </summary>
        /// <param name="requestURL">URL запроса.</param>
        /// <returns>Ответ от Elastic.</returns>
        public string Get(string requestURL)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_connectionConfig.baseURL);
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
        /// <param name="requestURL">URL запроса.</param>
        /// <param name="obj">Отправляемый объект.</param>
        /// <returns>Ответ от Elastic.</returns>
        public string Put(string requestURL, Object obj)
        {

            string jSonData = JsonConvert.SerializeObject(obj);
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_connectionConfig.baseURL);

                HttpContent httpContent = new StringContent(jSonData, Encoding.UTF8, "application/json");
                
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
