using ElasticsearchService.Entities;

namespace ElasticsearchService
{
    /// <summary>
    /// API для взаимодействия с Elastic.
    /// </summary>
    public class ElasticsearchAPI
    {
        /// <summary>
        /// Настрой соединения с Elastic.
        /// </summary>
        private ConnectionConfig _connectionConfig;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="connectionConfig">Настройки соединения с Elastic.</param>
        public ElasticsearchAPI(ConnectionConfig connectionConfig) 
        {
            _connectionConfig = connectionConfig;
        }

        /// <summary>
        /// Настройка конвейера для загрузки файлов в Elastic.
        /// </summary>
        /// <returns>Ответ от Elastic.</returns>
        public string? ConfiguratePipelineAttachmentAsync()
        {
            Request request = new Request(_connectionConfig);
            string requestURL = "_ingest/pipeline/attachment";

            PipelineAttachment pipelineAttachment = new PipelineAttachment();
            pipelineAttachment.processors = new[] { new Processor() };

            try
            {
                return request.Put(requestURL, pipelineAttachment);
            }
            catch (Exception)
            {
                return null;
            }        
        }

        /// <summary>
        /// Отправка файла в Elastic.
        /// </summary>
        /// <param name="fileName">Полное имя файла, связанного с одной страницей распознаваемого документа.</param>
        /// <param name="uploadKey">Уникальный ключ загружаемого файла.</param>
        /// <param name="totalPages">Общее количество страниц в распознаваемом документе.</param>
        /// <returns>Ответ от Elastic.</returns>
        public string SendFileContent(string fileName, string uploadKey, int totalPages)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string parseString = fileNameWithoutExtension.Substring(fileNameWithoutExtension.LastIndexOf("-") + 1);
            int pageNumber;
            try
            {
                pageNumber = Int32.Parse(parseString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не могу распознать номер страницы из '{parseString}'.\n {ex}");
                throw;
            }
            
            Request request = new Request(_connectionConfig);            
            string requestURL = $"{_connectionConfig.attachmentIndex}/_doc/{uploadKey}_{pageNumber}?pipeline=attachment";

            Data data = new Data();
            data.file = new Entities.FileInfo();
            data.file.name = Path.GetFileName(fileName);
            data.file.uploadKey = uploadKey;
            data.file.pageNumber = pageNumber;
            data.file.totalPages = totalPages;

            byte[] AsBytes = System.IO.File.ReadAllBytes(fileName);
            data.data = Convert.ToBase64String(AsBytes);

            try
            {
                string response = request.Put(requestURL, data);
                Console.WriteLine($"Файл успешно загружен в Elastic. Доступ по адресу: {_connectionConfig.attachmentIndex}/_doc/{uploadKey}_{pageNumber}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка Put-запроса: \n" + ex.Message);
                throw;
            }
        }
    }
}
