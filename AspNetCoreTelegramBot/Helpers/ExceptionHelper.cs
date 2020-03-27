using System;

namespace AspNetCoreTelegramBot.Helpers
{
    /// <summary>
    /// Хелпер исключений
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Проверка строки на пустоту или null
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="name">Имя параметра</param>
        public static void ThrowIfNullOrEmpty(string str, string name)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException($"{name} is null or empty");
            }
        }

        /// <summary>
        /// Проверка объекта на null
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="name">Имя параметра</param>
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{name} is null");
            }
        }
    }
}