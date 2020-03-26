using System;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Attributes
{
    public class CommandChatTypeAttribute : Attribute
    {
        public ChatType ChatType { get; set; }

        public CommandChatTypeAttribute(ChatType chatType)
        {
            ChatType = chatType;
        }
    }
}