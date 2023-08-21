namespace IIS.ReportsOcrAndSearch
{
    using System.IO;
    using System.Net;
    using ICSSoft.STORMNET;

    /// <summary>
    /// Класс для сохрания pdf файла в общее файловое хранилище и отправки запроса в OCR.
    /// </summary>
    public class FileTransferToOCR: IDataObjectUpdateHandler
    {
        private string ocrFileStoragePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTransferToOCR"/> class.
        /// </summary>
        /// <param name="ocrFileStoragePath">Путь в общем хранилище, которое доступно OCR сервису.</param>
        public FileTransferToOCR(string ocrFileStoragePath)
        {
            this.ocrFileStoragePath = ocrFileStoragePath;
        }

        /// <summary>
        /// Обработчик события обновления или создания объекта.
        /// </summary>
        /// <param name="dataObject">Измененный объек.</param>
        public void CallbackAfterUpdate(DataObject dataObject)
        {
            string typeName = dataObject.GetType().Name;

            if (typeName == "Report")
            {
                Report report = (Report)dataObject;

                string fileName = report.reportFile.Name;
                string saveDirectory = Path.Combine(ocrFileStoragePath, fileName);
                string url = report.reportFile.Url;

                using (var client = new WebClient())
                {
                    client.DownloadFile(url, saveDirectory);
                }
            }
        }
    }
}
