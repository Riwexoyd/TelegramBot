using System;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут типа команды
    /// Добавить атрибут на класс команды, если необходимо ограничить тип чата
    /// </summary>
    public class CommandChatTypeAttribute : Attribute
    {
        /// <summary>
        /// Тип чата
        /// </summary>
        public ChatType ChatType { get; set; }

        /// <summary>
        /// Конструктор атрибута
        /// </summary>
        /// <param name="chatType">Тип чата</param>
        public CommandChatTypeAttribute(ChatType chatType)
        {
            ChatType = chatType;
        }
    }
}