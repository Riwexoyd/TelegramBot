using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Проверка объекта на указанный тип
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="obj">Объект</param>
        public static void ThrowIfNotType<T>(object obj, string name)
        {
            if (!(obj is T))
            {
                throw new ArgumentException($"Object {name} is not type {typeof(T).Name}");
            }
        }

        /// <summary>
        /// Проверка на наличие ключа в дикшенери
        /// </summary>
        /// <param name="dictionary">Коллекция</param>
        /// <param name="key">Ключ</param>
        public static void ThrowIfNotContains(Dictionary<string, object> dictionary, string name, string key)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new ArgumentException($"Dictionary {name} is not contains key: {key}");
            }
        }
    }
}