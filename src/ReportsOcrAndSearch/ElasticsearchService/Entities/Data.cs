namespace ElasticsearchService.Entities
{
    /// <summary>
    /// Передаваемые данные файла и сопутствующая информация.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Имя свойства, значение которого будет представлять содержимое файла.
        /// </summary>
        public const string fieldDataName = "data";
        
        /// <summary>
        /// Содержимое файла в Base64 строковом формате, имя этого свойства должно совпадать со значением fieldDataName.
        /// </summary>
        public string data { get; set; } = String.Empty;

        /// <summary>
        /// Сопроводительная информация по файлу.
        /// </summary>
        public FileInfo? file { get; set; }
    }
}
