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
    }
}
