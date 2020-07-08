﻿using AspNetCoreTelegramBot.Extensions;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <inheritdoc/>
    public class NickNameGeneratorService : INickNameGeneratorService
    {
        private const string Url = "https://github.com/dwyl/english-words/archive/master.zip";
        private const string ArchiveDirectoryName = "english-words-master";
        private const string FileName = "words_alpha.txt";

        private readonly char[] symbols = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };

        private Random rand = new Random();
        private Dictionary<char, HashSet<string>> parts = new Dictionary<char, HashSet<string>>();
        private HashSet<string> hashWords = new HashSet<string>();
        private bool initialized = false;

        private IHttpDownloadService httpDownloadService;

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

            var archivePath = await httpDownloadService.DownloadFile(Url);
            var dir = UnZip(archivePath);
            File.Delete(archivePath);

            var path = Path.Combine(dir, ArchiveDirectoryName, FileName);

            foreach (var s in symbols)
            {
                parts.Add(s, new HashSet<string>());
            }

            var words = File.ReadAllLines(path);

            foreach (var word in words)
            {
                foreach (var part in GetParts(word.ToLower()))
                {
                    parts[part[0]].Add(part.Substring(1));
                }
                hashWords.Add(word.ToLower());
            }

            initialized = true;
        }

        private List<string> GetParts(string word)
        {
            //ab
            //bac
            //cin
            //nat
            //tion

            //batt
            //teaux
            List<string> parts = new List<string>();
            int lastIndex = 0;
            for (int i = 0; i < word.Length; ++i)
            {
                //  если согласная и следующая не такая же
                if (!vowels.Contains(word[i]) && i + 1 < word.Length && word[i] != word[i + 1])
                {
                    if (i == 0) continue;
                    //  добавляем часть слова без гласной
                    var part = word.Substring(lastIndex, i - lastIndex + 1);
                    parts.Add(part);
                    lastIndex = i;
                }
            }
            //  если последняя буква согласная
            if (lastIndex != word.Length - 1)
            {
                //  если все еще первое слово
                if (parts.Count == 0)
                {
                    //  игнорим
                    //  parts.Add(word);
                }
                else
                {
                    parts.Add(word.Substring(lastIndex, word.Length - lastIndex));
                }
            }

            return parts;
        }

        /// <summary>
        /// Распаковать
        /// </summary>
        /// <param name="path">Путь до архива</param>
        /// <returns>Путь до папки</returns>
        private string UnZip(string path)
        {
            var directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString().ToPathCorrect());
            Directory.CreateDirectory(directory);
            ZipFile.ExtractToDirectory(path, directory);
            return directory;
        }

        /// <inheritdoc/>
        public string GenerateNickName(char[] chars)
        {
            var arr = chars != null && chars.Length != 0 ? chars : symbols;
            var result = string.Empty;
            while (string.IsNullOrEmpty(result)
                || hashWords.Contains(result.ToLower())
                || !IsCorrect(result))
            {
                result = Build(arr);
            }

            return result;
        }

        private string Build(char[] chars)
        {
            var builder = new StringBuilder();
            int length = 5 + rand.Next(10);
            builder.Append(char.ToUpper(chars[rand.Next(chars.Length)]));
            while (builder.Length < length)
            {
                var last = char.ToLower(builder[builder.Length - 1]);
                var pairs = parts[last].ToList();
                string part = null;
                while (string.IsNullOrEmpty(part)
                    || vowels.Contains(last) && vowels.Contains(part[0])
                    || builder.Length + part.Length > length + 2)
                {
                    part = pairs[rand.Next(pairs.Count)];
                }

                builder.Append(part);
            }

            return builder.ToString();
        }

        private bool IsCorrect(string nickName)
        {
            nickName = nickName.ToLower();
            return !ContainsThreeAndMoreSameSymbols(nickName)
                && LessOrEqualTwo(nickName);
        }

        private bool ContainsThreeAndMoreSameSymbols(string nickName)
        {
            char last = nickName[0];
            int count = 1;
            for (int i = 1; i < nickName.Length; ++i)
            {
                if (last == nickName[i])
                {
                    ++count;
                    if (count > 2)
                    {
                        return true;
                    }
                }
                else
                {
                    last = nickName[i];
                    count = 1;
                }
            }

            return false;
        }

        private bool LessOrEqualTwo(string name)
        {
            List<int> positions = new List<int>();
            for (int i = 0; i < name.Length - 1; ++i)
            {
                if (name[i] == name[i + 1])
                {
                    positions.Add(i);
                }
            }

            Func<bool> func = () =>
            {
                if (positions.Count < 2) return false;
                return !(positions[1] - positions[0] > 2);
            };

            return positions.Count <= 2 && !func();
        }

        /// <summary>
        /// Проверка на похожие слова
        /// </summary>
        /// <param name="name">Никнэйм для проверки</param>
        /// <param name="similar">Похожие слова</param>
        /// <returns>True, если есть похожие слова</returns>
        public bool Antiplagiat(string name, out IList<string> similar)
        {
            similar = new List<string>();

            var words = hashWords.Where(i => i.Length == name.Length);
            foreach (var word in words)
            {
                int count = 0;
                for (int i = 0; i < word.Length; ++i)
                {
                    if (name[i] == word[i])
                        ++count;
                }

                if ((float)count / name.Length >= 0.6)
                    similar.Add(word);
            }

            return similar.Count == 0;
        }

        /// <summary>
        /// Проверка на содержание целых слов в нике
        /// </summary>
        /// <param name="name">Ник</param>
        /// <param name="container">Список слов внутри ника</param>
        /// <returns>True, если содержит другие слова в нике</returns>
        public bool ContainsAnotherWords(string name, out IList<string> container)
        {
            container = new List<string>();
            var words = hashWords.Where(i => i.Length < name.Length && i.Length > 2);
            foreach (var word in words)
            {
                if (name.Contains(word))
                {
                    container.Add(word);
                }
            }

            return container.Count != 0;
        }

        /// <inheritdoc/>
        public string GetNickNameInformation(string nickName)
        {
            nickName = nickName.ToLower();
            var info = new StringBuilder();
            var antiplagiat = Antiplagiat(nickName, out IList<string> similar);
            var anotherWords = ContainsAnotherWords(nickName, out IList<string> contains);
            if (!antiplagiat)
            {
                info.AppendLine("Найдены похожие слова:");
                info.AppendLine(string.Join("\n", similar));
            }
            else
            {
                info.AppendLine("Похожих слов не найдено");
            }

            if (anotherWords)
            {
                info.AppendLine("Содержит в себе слова:");
                info.AppendLine(string.Join("\n", contains));
            }
            else
            {
                info.AppendLine("Не содержит в себе других слов");
            }

            return info.ToString();
        }
    }
}