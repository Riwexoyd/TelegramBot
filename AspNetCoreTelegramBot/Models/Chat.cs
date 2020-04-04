using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Models
{
    public class Chat : IEquatable<Chat>
    {
        public int Id { get; set; }

        public long TelegramId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ChatType TelegramChatType { get; set; }

        public DateTime RegisterDate { get; set; }

        public ICollection<UserChat> UserChats { get; set; }

        /// <summary>
        /// Чаты (для приватных категорий)
        /// </summary>
        public ICollection<RouletteCategoryChat> WinnerCategoryChats { get; set; }

        public Chat()
        {
            UserChats = new List<UserChat>();
            WinnerCategoryChats = new List<RouletteCategoryChat>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Chat);
        }

        public bool Equals([AllowNull] Chat other)
        {
            return other != null &&
                   Id == other.Id &&
                   TelegramId == other.TelegramId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TelegramId);
        }

        public static bool operator ==(Chat left, Chat right)
        {
            return EqualityComparer<Chat>.Default.Equals(left, right);
        }

        public static bool operator !=(Chat left, Chat right)
        {
            return !(left == right);
        }
    }
}