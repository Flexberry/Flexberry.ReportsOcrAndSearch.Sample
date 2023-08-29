namespace ElasticsearchService.Entities
{
    /// <summary>
    /// Процессор.
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Настройки для прикрепляемого файла.
        /// </summary>
        public Attachment attachment { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public Processor() 
        {
            attachment = new Attachment();
        }
    }
}
