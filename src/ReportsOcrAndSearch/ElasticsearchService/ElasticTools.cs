namespace ElasticsearchService
{
    using ElasticsearchService.Entities;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Configuration;

    /// <summary>
    /// Методы для взаимодействия с Elastic.
    /// </summary>
    public class ElasticTools
    {
        /// <summary>
        /// Адрес сервера Elastic.
        /// </summary>
        private readonly string elasticUrl;
        
        /// <summary>
        /// Индекс, связанный с загружаемыми документами.
        /// </summary>
        private readonly string documentIndex;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="connectionConfig">Настройки соединения с Elastic.</param>
        public ElasticTools(IConfiguration connectionConfig) 
        {
            elasticUrl = connectionConfig["ElasticUrl"];
            if (string.IsNullOrEmpty(elasticUrl))
            {
                throw new ConfigurationErrorsException("ElasticUrl is not specified in Configuration or enviromnent variables.");
            }

            documentIndex = connectionConfig["ElasticDocumentsIndex"];
            if (string.IsNullOrEmpty(documentIndex))
            {
                throw new ConfigurationErrorsException("ElasticDocumentsIndex is not specified in Configuration or enviromnent variables.");
            }
        }

        /// <summary>
        /// Настройка конвейера для загрузки файлов в Elastic.
        /// </summary>
        public void ConfiguratePipelineAttachment()
        {
            string requestUrl = "_ingest/pipeline/attachment";

            // Поле field должно иметь значение как имя поля в методе SendFileContent
            JObject jsonBody = new JObject(
                new JProperty("description", "Recognized files"),
                new JProperty(
                    "processors", 
                    new JArray(
                        new JObject(
                            new JProperty(
                                "attachment", 
                                new JObject(
                                    new JProperty("field", "data")))))));
            string jsonData = jsonBody.ToString(); 

            try
            {
                Request.SendPutRequest(elasticUrl, requestUrl, jsonData);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Pipeline attachment configutated error!\n" + ex.Message);
            }        
        }

        /// <summary>
        /// Отправка файла в Elastic. 
        /// После распознания файлы представлены в следущем формате 'имя файла без расширения-номер страницы.txt'.
        /// Например, после распознания файла 'myfile.pdf', состоящего из 2 страниц, 
        /// получаем файлы с распознанным постранично текстом: 'myfile-0.txt' и 'myfile-1.txt'. 
        /// </summary>
        /// <param name="fileName">Полное имя файла, связанного с одной страницей распознаваемого документа.</param>
        /// <param name="uploadKey">Уникальный ключ загружаемого файла.</param>
        /// <param name="totalPages">Общее количество страниц в распознаваемом документе.</param>
        public void SendFileContent(string fileName, string uploadKey, int totalPages)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string parseString = fileNameWithoutExtension.Substring(fileNameWithoutExtension.LastIndexOf("-") + 1);
            int pageNumber;
            try
            {
                pageNumber = Int32.Parse(parseString) + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не могу распознать номер страницы из '{parseString}'.\n {ex}");
                throw new ArgumentException("File content sended error! Page number not valid.\n" + ex.Message);
            }

            string fileUrl = $"{documentIndex}/_doc/{uploadKey}_{pageNumber}";
            string requestUrl = $"{fileUrl}?pipeline=attachment";

            FileInfo fileInfo = new FileInfo(
                name: Path.GetFileName(fileName),
                uploadKey: uploadKey,
                pageNumber: pageNumber,
                totalPages: totalPages);

            byte[] fileBytes = System.IO.File.ReadAllBytes(fileName);

            // Имя поля "data" должно совпадать со значением "field", заданным в методе ConfiguratePipelineAttachment
            JObject jsonBody = new JObject(
                new JProperty("data", Convert.ToBase64String(fileBytes)),
                new JProperty("file", JObject.Parse(JsonConvert.SerializeObject(fileInfo))));
            string jsonData = jsonBody.ToString();

            try
            {
                Request.SendPutRequest(elasticUrl, requestUrl, jsonData);
                Console.WriteLine($"Файл успешно загружен в Elastic. Доступ по адресу: {fileUrl}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка Put-запроса на загрузку данных файла в Elastic!\n" + ex.Message);
                throw new HttpRequestException("File content sended error!\n" + ex.Message);
            }
        }

        /// <summary>
        /// Удаление данных по файлу из Elastic по заданному uploadKey.
        /// Имя поля (file.UploadKey), по которому происходит поиск, 
        /// совпадает с именем, заданным при отправке данных в методе SendFileContent.
        /// </summary>
        /// <param name="uploadKey">Уникальный идентификатор файла.</param>
        public void DeleteFileByUplodKey(string uploadKey)
        {
            string requestUrl = $"{documentIndex}/_delete_by_query";

            JObject jsonBody = new JObject(
                new JProperty(
                    "query", 
                    new JObject(
                        new JProperty(
                            "match",
                            new JObject(
                                new JProperty("file.UploadKey", uploadKey))))));
            string jsonData = jsonBody.ToString();

            try
            {
                Request.SendPostRequest(elasticUrl, requestUrl, jsonData);
                Console.WriteLine($"Успешно удалены данные из Elastic по файлу.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка Post-запроса на удаление данных по файлу из Elastic!\n" + ex.Message);
                throw new HttpRequestException("File delete request error!\n" + ex.Message);
            }
        }
    }
}