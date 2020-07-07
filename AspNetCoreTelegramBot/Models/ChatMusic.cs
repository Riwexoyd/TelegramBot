namespace AspNetCoreTelegramBot.Models
{
    public class ChatMusic
    {
        public int Id { get; set; }

        public Chat Chat { get; set; }

        public Music Music { get; set; }
    }
}