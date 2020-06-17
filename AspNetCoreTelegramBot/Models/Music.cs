using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Музыка
    /// </summary>
    public class Music
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Музыка по чату
        /// </summary>
        public virtual IEnumerable<ChatMusic> ChatMusics { get; set; }

        public Music()
        {
            ChatMusics = new List<ChatMusic>();
        }
    }
}