namespace AspNetCoreTelegramBot.Enums
{
    public enum CallbackQueryType
    {
        /// <summary>
        /// Публичный query, запросы обрабатываются от всех пользователей
        /// </summary>
        Public,

        /// <summary>
        /// Запрос обрабатывается только в том случае, если он отправлен от создателя
        /// </summary>
        SenderOnly
    }
}