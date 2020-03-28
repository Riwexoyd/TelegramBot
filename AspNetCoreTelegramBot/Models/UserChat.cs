using System;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Чаты пользователей
    /// </summary>
    public class UserChat
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }

        /// <summary>
        /// Участник рулетки
        /// </summary>
        public bool IsRouletteParticipant { get; set; }

        /// <summary>
        /// Дата присоединения
        /// </summary>
        public DateTime RouletteJoinDate { get; set; }
    }
}