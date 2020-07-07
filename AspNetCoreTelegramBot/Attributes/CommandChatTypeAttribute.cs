using System;
using System.Collections.Generic;
using System.Linq;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут типа доступа к команде.
    /// Добавить атрибут на класс команды, если необходимо ограничить тип чата.
    /// </summary>
    public class CommandChatTypeAttribute : Attribute
    {
        /// <summary>
        /// Типы чата
        /// </summary>
        public ICollection<ChatType> ChatTypes { get; set; }

        /// <summary>
        /// Конструктор атрибута
        /// </summary>
        /// <param name="chatType">Тип чата</param>
        public CommandChatTypeAttribute(params ChatType[] chatTypes)
        {
            ChatTypes = chatTypes.ToList();
        }
    }
}