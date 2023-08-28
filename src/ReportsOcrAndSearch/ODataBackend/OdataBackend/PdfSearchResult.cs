namespace IIS.ReportsOcrAndSearch.OdataBackend.RequestObjects
{
    /// <summary>
    /// Результат поиска документов.
    /// </summary>
    public class PdfSearchResult
    {
        private string fileName;
        private string uploadKey;
        private string uploadUrl;
        private string pageNumber;
        private string totalPages;

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get => fileName; set => fileName = value; }

        /// <summary>
        /// Ключ файла.
        /// </summary>
        public string UploadKey { get => uploadKey; set => uploadKey = value; }

        /// <summary>
        /// Путь загрузки файла.
        /// </summary>
        public string UploadUrl { get => uploadUrl; set => uploadUrl = value; }

        /// <summary>
        /// Номер страницы.
        /// </summary>
        public string PageNumber { get => pageNumber; set => pageNumber = value; }

        /// <summary>
        /// Всего страниц.
        /// </summary>
        public string TotalPages { get => totalPages; set => totalPages = value; }

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
            this.fileName = fileName;
            this.uploadKey = uploadKey;
            this.uploadUrl = uploadUrl;
            this.pageNumber = pageNumber;
            this.totalPages = totalPages;
        }
    }
}
