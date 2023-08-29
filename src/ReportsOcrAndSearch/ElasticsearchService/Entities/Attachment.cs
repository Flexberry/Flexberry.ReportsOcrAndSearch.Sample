using Newtonsoft.Json;

namespace ElasticsearchService.Entities
{
    /// <summary>
    /// Настройки для прикрепляемого файла.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Связанно с именем свойства, значение которого будет представлять содержимое файла.
        /// </summary>
        [JsonProperty]
        public const string field = Data.fieldDataName;

        /// <summary>
        /// Параметр определяющий удалить (true) или сохранить (false) двоичное представление файла.
        /// </summary>
        public bool remove_binary { get; set; } = false;
    }
}
