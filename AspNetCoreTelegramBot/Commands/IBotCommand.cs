using System.Threading.Tasks;

using Chat = AspNetCoreTelegramBot.Models.Chat;
using User = AspNetCoreTelegramBot.Models.User;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Команда бота
    /// </summary>
    public interface IBotCommand
    {
        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="chat">Чат</param>
        /// <returns>Task</returns>
        Task ExecuteAsync(User sender, Chat chat);
    }
}