using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <inheritdoc/>
    public class HttpDownloadService : IHttpDownloadService
    {
        /// <inheritdoc/>
        public async Task<string> DownloadFile(string url)
        {
            var path = Path.GetTempFileName();
            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(new Uri(url), path);
            }

            return path;
        }
    }
}