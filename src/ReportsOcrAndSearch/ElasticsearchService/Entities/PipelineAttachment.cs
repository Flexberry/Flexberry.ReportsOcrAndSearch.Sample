namespace ElasticsearchService.Entities
{
    /// <summary>
    /// Параметры конвейера приема данных.
    /// </summary>
    public class PipelineAttachment
    {
        /// <summary>
        /// Описание.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// Процессоры.
        /// </summary>
        public Processor[]? processors;
    }
}
