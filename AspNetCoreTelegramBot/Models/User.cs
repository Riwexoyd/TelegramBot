using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCoreTelegramBot.Models
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }

        public int TelegramId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Логин для входа на сайт
        /// </summary>
        public string Login { get; set; }

        public ICollection<UserChat> UserChats { get; set; }

        public User()
        {
            UserChats = new List<UserChat>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals([AllowNull] User other)
        {
            return other != null &&
                   Id == other.Id &&
                   TelegramId == other.TelegramId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TelegramId);
        }

        public static bool operator ==(User left, User right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }
}