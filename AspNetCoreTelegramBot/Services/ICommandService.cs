using AspNetCoreTelegramBot.Models;

using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис команд телеграм бота
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Инициализировать сервис команд асинхронно
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// Проверка текста на команду
        /// </summary>
        /// <param name="command">Текст команды</param>
        /// <returns>true - команда, false - нет</returns>
        bool IsCommand(string command);

        /// <summary>
        /// Обработать текстовую команду
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="chat">Чат</param>
        /// <param name="commandText">Текст команды</param>
        /// <returns>Task</returns>
        Task HandleTextCommand(User user, Chat chat, string commandText);
    }
}