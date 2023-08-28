namespace IIS.ReportsOcrAndSearch.ElasticPdfSearch
{
    /// <summary>
    /// Результат поиска документов.
    /// </summary>
    public class PdfSearchResult
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Ключ файла.
        /// </summary>
        public string UploadKey { get; set; }

        /// <summary>
        /// Путь загрузки файла.
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public string PageNumber { get; set; }

        /// <summary>
        /// Всего страниц.
        /// </summary>
        public string TotalPages { get; set; }

        /// <summary>
        /// Результат поиска документов.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="uploadKey">Ключ файла.</param>
        /// <param name="uploadUrl">Путь загрузки файла.</param>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="totalPages">Всего страниц.</param>
        public PdfSearchResult(string fileName, string uploadKey, string uploadUrl, string pageNumber, string totalPages)
        {
            FileName = fileName;
            UploadKey = uploadKey;
            UploadUrl = uploadUrl;
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }
    }
}
