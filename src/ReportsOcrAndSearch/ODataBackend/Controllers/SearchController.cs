namespace IIS.ReportsOcrAndSearch.Controllers
{
    using System.Collections.Generic;
    using IIS.ReportsOcrAndSearch.OdataBackend;
    using IIS.ReportsOcrAndSearch.OdataBackend.RequestObjects;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    /// <summary>
    /// Контроллер поиска документов.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IConfiguration config;
        private readonly ElasticTools elasticTools = null;

        /// <summary>
        /// Контроллер поиска документов.
        /// </summary>
        /// <param name="config">Конфигурация.</param>
        public SearchController(IConfiguration config)
        {
            this.config = config;
            elasticTools = new ElasticTools(this.config);
        }

        /// <summary>
        /// Поиск документа.
        /// </summary>
        /// <param name="searchText">Текст для поиска.</param>
        /// <returns>Результаты поиска.</returns>
        [HttpGet("[action]")]
        public string SearchDocuments([FromQuery] string searchText)
        {
            List<PdfSearchResult> lst = elasticTools.SearchDocuments(searchText);
            string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

            return json;
        }
    }
}
