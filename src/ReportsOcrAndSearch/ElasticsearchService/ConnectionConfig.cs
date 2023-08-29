namespace ElasticsearchService
{
    /// <summary>
    /// Настройки соединения с Elastic для отправки файлов.
    /// </summary>
    public class ConnectionConfig
    {
        /// <summary>
        /// Адрес сервера Elastic.
        /// </summary>
        public string baseURL { get; set; }
        
        /// <summary>
        /// Связанный индекс для хранения файлов.
        /// </summary>
        public string attachmentIndex { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="baseUrl">Адрес сервера Elastic.</param>
        /// <param name="attachmentIndex">Связанный индекс для хранения файлов.</param>
        public ConnectionConfig(string baseUrl, string attachmentIndex)
        {
            this.baseURL = baseUrl;
            this.attachmentIndex = attachmentIndex;
        }
    }
}
