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
        public string? name { get; set; }

        /// <summary>
        /// Уникальный ключ загруженного файла.
        /// </summary>
        public string? uploadKey { get; set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int? pageNumber { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int? totalPages { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя файла.</param>
        /// <param name="uploadKey">Уникальный ключ загруженного файла.</param>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="totalPages">Общее количество страниц.</param>
        public FileInfo(string name, string uploadKey, int pageNumber, int totalPages)
        {
            this.name = name;
            this.uploadKey = uploadKey;
            this.pageNumber = pageNumber;
            this.totalPages = totalPages;
        }
    }
}