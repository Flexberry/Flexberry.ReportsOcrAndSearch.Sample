namespace IIS.ReportsOcrAndSearch.Controllers.RequestObjects
{
    /// <summary>
    /// Результат поиска документов.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName;

        /// <summary>
        /// Ключ файла.
        /// </summary>
        public string UploadKey;

        /// <summary>
        /// Путь загрузки файла.
        /// </summary>
        public string UploadUrl;

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public string PageNumber;

        /// <summary>
        /// Всего страниц.
        /// </summary>
        public string TotalPages;

        /// <summary>
        /// Результат поиска документов.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="uploadKey">Ключ файла.</param>
        /// <param name="uploadUrl">Путь загрузки файла.</param>
        /// <param name="pageNumber">Номер страницы.</param>
        /// <param name="totalPages">Всего страниц.</param>
        public SearchResult(string fileName, string uploadKey, string uploadUrl, string pageNumber, string totalPages)
        {
            FileName = fileName;
            UploadKey = uploadKey;
            UploadUrl = uploadUrl;
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }
    }
}
