using AspNetCoreTelegramBot.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <inheritdoc/>
    public class NickNameGeneratorService : INickNameGeneratorService
    {
        const string Url = "https://github.com/dwyl/english-words/archive/master.zip";
        const string ArchiveDirectoryName = "english-words-master";
        const string FileName = "words_alpha.txt";

        private bool initialized = false;
        private readonly Mutex mutex = new Mutex();

        IHttpDownloadService httpDownloadService;

        public NickNameGeneratorService(IHttpDownloadService httpDownloadService)
        {
            this.httpDownloadService = httpDownloadService;
        }

        /// <summary>
        /// Инициализировать генератор
        /// </summary>
        /// <returns>Task</returns>
        public async Task InitializeGenerator()
        {
            if (initialized)
            {
                return;
            }
            mutex.WaitOne();
            if (initialized)
            {
                mutex.ReleaseMutex();
                return;
            }

            var archivePath = await httpDownloadService.DownloadFile(Url);
            var dir = UnZip(archivePath);
            File.Delete(archivePath);


            initialized = true;
            mutex.ReleaseMutex();
        }

        /// <summary>
        /// Распаковать
        /// </summary>
        /// <param name="path">Путь до архива</param>
        /// <returns>Путь до папки</returns>
        private string UnZip(string path)
        {
            var directory = Path.Combine(Environment.CurrentDirectory, Guid.NewGuid().ToString().ToPathCorrect());
            Directory.CreateDirectory(directory);
            ZipFile.ExtractToDirectory(path, directory);
            return directory;
        }

        /// <inheritdoc/>
        public async Task<string> GenerateNickName()
        {
            await InitializeGenerator();
            return string.Empty;
        }

        /// <inheritdoc/>
        public async Task<string> GetNickNameInformation(string nickName)
        {
            await InitializeGenerator();
            return string.Empty;
        }
    }
}
