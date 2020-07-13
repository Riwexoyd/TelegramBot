using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис загрузки файлов по http
    /// </summary>
    public interface IHttpDownloadService
    {
        /// <summary>
        /// Загрузить файл по url
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Путь до локального файла</returns>
        Task<string> DownloadFile(string url);
    }
}