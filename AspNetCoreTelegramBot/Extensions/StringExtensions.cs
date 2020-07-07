using AspNetCoreTelegramBot.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Extensions
{
    /// <summary>
    /// Расширения для строк
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Заменить в строке недопустимые для пути символы на '-'
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Корректная строка</returns>
        public static string ToPathCorrect(this string input)
        {
            ExceptionHelper.ThrowIfNull(input, "input");

            var fileNameChars = Path.GetInvalidFileNameChars();
            var invalidChars = fileNameChars.Union(Path.GetInvalidPathChars()).ToArray();

            return string.Join("-", input.Split(invalidChars));
        }
    }
}
