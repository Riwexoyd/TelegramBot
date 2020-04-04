using System;

namespace AspNetCoreTelegramBot.Models
{
    /// <summary>
    /// Победители рулетки
    /// </summary>
    public class RouletteWinner
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public RouletteCategoryChat RouletteCategoryChat { get; set; }
    }
}