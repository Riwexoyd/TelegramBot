using AspNetCoreTelegramBot.Models;

using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.TextHandlers
{
    /// <summary>
    /// Обработчик текстовых сообщений
    /// </summary>
    public interface ITextHandler
    {
        /// <summary>
        /// Обработать текстовое сообщение
        /// Лайфхак: вернуть false, чтобы остальные обработчики также его обработали
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="chat">Чат</param>
        /// <param name="text">Текст</param>
        /// <returns>Успешность обработки</returns>
        public Task<bool> HandleAsync(User sender, Chat chat, string text);
    }
}