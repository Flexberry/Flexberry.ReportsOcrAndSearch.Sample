namespace IIS.ReportsOcrAndSearch
{
    using System.IO;
    using System.Text.RegularExpressions;
    using ICSSoft.STORMNET;

    /// <summary>
    /// Класс для сохрания pdf файла в общее файловое хранилище и отправки запроса в OCR.
    /// </summary>
    public class FileTransferToOcr : IDataObjectUpdateHandler
    {
        private string fileUploadPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTransferToOcr"/> class.
        /// </summary>
        /// <param name="fileUploadPath">Путь в общем хранилище, которое доступно OCR сервису.</param>
        public FileTransferToOcr(string fileUploadPath)
        {
            this.fileUploadPath = fileUploadPath;
        }

        /// <summary>
        /// Обработчик события обновления или создания объекта.
        /// </summary>
        /// <param name="dataObject">Измененный объек.</param>
        public void CallbackAfterUpdate(DataObject dataObject)
        {
            if (dataObject == null)
            {
                return;
            }

            if (typeof(Report) == dataObject.GetType())
            {
                Report report = (Report)dataObject;

                string fileName = report.reportFile.Name;
                string url = report.reportFile.Url;

                Regex regex = new Regex("fileUploadKey=(.*?)&");
                var regexMatch = regex.Match(url).Groups;

                if (regexMatch != null && regexMatch.Count > 1)
                {
                    string uploadKey = regex.Match(url).Groups[1].ToString();
                    string filePath = Path.Combine(fileUploadPath, uploadKey, fileName);

                    // Вызов OCR сервиса, с передачей пути к распознаваемому файлу(filePath).
                }
            }
        }
    }
}
