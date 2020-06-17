using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Enums.Extensions
{
    /// <summary>
    /// Расширения для перечисления ChatType
    /// </summary>
    public static class ChatTypeExtensions
    {
        /// <summary>
        /// Получить полное имя для типа чата
        /// </summary>
        /// <param name="chatType">Тип чата</param>
        /// <returns>Полное имя</returns>
        public static string GetFullName(this ChatType chatType)
        {
            string name = chatType switch
            {
                ChatType.Private => "Личные сообщения",
                ChatType.Group => "Группа",
                ChatType.Channel => "Канал",
                ChatType.Supergroup => "Супер группа",
                _ => "Неизвестный тип чата",
            };

            return name;
        }
    }
}
