using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Категории рулетки
    /// </summary>
    public class RouletteCategory : IEquatable<RouletteCategory>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок категории
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Публичная категория
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Чаты
        /// </summary>
        public ICollection<RouletteCategoryChat> RouletteCategoryChats { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        public RouletteCategory()
        {
            RouletteCategoryChats = new List<RouletteCategoryChat>();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RouletteCategory);
        }

        public bool Equals(RouletteCategory other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(RouletteCategory left, RouletteCategory right)
        {
            return EqualityComparer<RouletteCategory>.Default.Equals(left, right);
        }

        public static bool operator !=(RouletteCategory left, RouletteCategory right)
        {
            return !(left == right);
        }
    }
}