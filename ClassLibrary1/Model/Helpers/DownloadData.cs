namespace MockarooLibrary.Model.Helpers
{
    /// <summary>
    /// Предоставляет удобную обвязяку для скачаивания файла пользователем
    /// А именно : filestream и название файла
    /// </summary>
    public class DownloadData
    {
        /// <summary>
        /// Расширение файла
        /// </summary>
        public string FileExtensionName { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public Stream FileStream { get; set; }

        /// <summary>
        /// Сгенерированные данные в формате строки
        /// </summary>
        public string Content { get; set; }
    }
}