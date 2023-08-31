namespace IIS.ReportsOcrAndSearch
{
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using ICSSoft.STORMNET;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Класс для сохрания pdf файла в общее файловое хранилище и отправки запроса в OCR.
    /// </summary>
    public class FileTransferToOcr : IDataObjectUpdateHandler
    {
        private readonly IConfiguration config;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="config">Настройки.</param>
        public FileTransferToOcr(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Обработчик события обновления или создания объекта.
        /// </summary>
        /// <param name="dataObject">Измененный объект.</param>
        public void CallbackAfterUpdate(DataObject dataObject)
        {
            if (dataObject == null)
            {
                throw new ArgumentNullException(nameof(dataObject));
            }

            if (dataObject.GetType() == typeof(Report))
            {
                Report report = (Report)dataObject;

                string fileName = report.reportFile.Name;
                string url = report.reportFile.Url;

                Regex regex = new Regex("fileUploadKey=(.*?)&");
                GroupCollection regexMatch = regex.Match(url).Groups;

                if (regexMatch != null && regexMatch.Count > 1)
                {
                    string uploadKey = regex.Match(url).Groups[1].ToString();
                    SendFileToOCRService(uploadKey, fileName);
                }
            }
        }

        /// <summary>
        /// Отправка запроса OCR-сервису на распознование содержимого файла и добавления в Elastic.
        /// http://localhost:6600/api/OcrRecognizer/RunRecognizeUploadedPdf?uploadDirectory=...&uploadKey=...&fileName=...
        /// </summary>
        /// <param name="uploadKey">Уникальный идентификатор файла.</param>
        /// <param name="fileName">Имя файла.</param>
        public void SendFileToOCRService(string uploadKey, string fileName)
        {
            string ocrServer = config["OCRServiceUrl"];
            if (string.IsNullOrEmpty(ocrServer))
            {
                throw new ConfigurationErrorsException("OCRServiceUrl is not specified in Configuration or enviromnent variables.");
            }

            string uploadDirectory = config["UploadUrl"];
            if (string.IsNullOrEmpty(uploadDirectory))
            {
                throw new ConfigurationErrorsException("UploadUrl is not specified in Configuration or enviromnent variables.");
            }

            string requestUrl = $"api/OcrRecognizer/RunRecognizeUploadedPdf" +
                $"?uploadDirectory={uploadDirectory}" +
                $"&uploadKey={uploadKey}" +
                $"&fileName={fileName}";

            using (HttpClient client = new HttpClient())
            {
                Uri baseAddress = new Uri(ocrServer);

                try
                {
                    using (HttpResponseMessage response = client.PostAsync(new Uri(baseAddress, requestUrl), null).Result)
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
                catch (Exception ex)
                {
                    LogService.LogError("File to OSR-service sended error!\n" + ex.Message);
                }
            }
        }
    }
}