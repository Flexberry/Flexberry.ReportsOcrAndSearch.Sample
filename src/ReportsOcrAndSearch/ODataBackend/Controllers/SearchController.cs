namespace IIS.ReportsOcrAndSearch.Controllers
{
    using System.Collections.Generic;
    using IIS.ReportsOcrAndSearch;
    using IIS.ReportsOcrAndSearch.Controllers.RequestObjects;
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
        private readonly IConfiguration _config;
        private readonly ElasticTools elasticTools = null;

        /// <summary>
        /// Контроллер поиска документов.
        /// </summary>
        /// <param name="config">Конфигурация.</param>
        public SearchController(IConfiguration config)
        {
            _config = config;
            elasticTools = new ElasticTools(_config);
        }

        /// <summary>
        /// Поиск документа.
        /// </summary>
        /// <param name="searchText">Текст для поиска.</param>
        /// <returns>Результаты поиска.</returns>
        [HttpGet("[action]")]
        public string SearchDocuments([FromQuery] string searchText)
        {
            List<SearchResult> lst = elasticTools.SearchDocuments(searchText);
            string json = JsonConvert.SerializeObject(lst, Formatting.Indented);

            return json;
        }
    }
}
