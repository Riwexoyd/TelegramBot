using AspNetCoreTelegramBot.Commands;
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
        /// Проверка на существование команды
        /// </summary>
        /// <param name="command">Текст команды</param>
        /// <returns>true - есть такая команда, false - нет такой команды</returns>
        bool ContainsCommand(string command);

        /// <summary>
        /// Получить объект команды. Если команды не существует, то создаст исключение
        /// </summary>
        /// <param name="command">Текст команды</param>
        /// <returns>Объект команды</returns>
        IBotCommand GetCommand(string command);

        /// <summary>
        /// Проверка текста на команду
        /// </summary>
        /// <param name="command">Текст команды</param>
        /// <returns>true - команда, false - нет</returns>
        bool IsCommand(string command);

        /// <summary>
        /// Проверить команду на возможность выполнения
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="chat">Чат</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        /// <returns>True, если команду можно выполнить; Иначе False</returns>
        public bool CanExecuteCommand(IBotCommand command, Chat chat, out string errorMessage);
    }
}