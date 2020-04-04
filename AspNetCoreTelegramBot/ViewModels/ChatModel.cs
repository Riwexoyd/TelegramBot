using System;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.ViewModels
{
    public class ChatModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ChatType TelegramChatType { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}