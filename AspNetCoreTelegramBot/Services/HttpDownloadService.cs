using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
