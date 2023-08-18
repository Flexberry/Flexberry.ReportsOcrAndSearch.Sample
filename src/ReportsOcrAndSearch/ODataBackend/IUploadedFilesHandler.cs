namespace IIS.ReportsOcrAndSearch
{
    using ICSSoft.STORMNET;

    /// <summary>
    /// Интерфейс для обработки событий изменения DataObject. В данном случае это нужно, чтобы отправить загруженный файл в OCR.
    /// </summary>
    public interface IUploadedFilesHandler
    {
        /// <summary>
        /// Обработчик события обновления или создания объекта.
        /// </summary>
        /// <param name="dataObject">Измененный объект.</param>
        public void CallbackAfterUpdate(DataObject dataObject);
    }
}
