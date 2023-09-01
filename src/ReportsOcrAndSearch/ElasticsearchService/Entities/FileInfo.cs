namespace ElasticsearchService.Entities
{
    /// <summary>
    /// Сопуствующая информация о файле.
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Имя распознанного файла.
        /// </summary>
        public string TxtFileName { get; set; }

        /// <summary>
        /// Уникальный ключ загруженного файла.
        /// </summary>
        public string UploadKey { get; set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя файла.</param>
        /// <param name="txtFileName">Имя распознанного файла.</param>
        /// <param name="uploadKey">Уникальный ключ загруженного файла.</param>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="totalPages">Общее количество страниц.</param>
        public FileInfo(string name, string txtFileName, string uploadKey, int pageNumber, int totalPages)
        {
            FileName = name;
            TxtFileName = txtFileName;
            UploadKey = uploadKey;
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }
    }
}