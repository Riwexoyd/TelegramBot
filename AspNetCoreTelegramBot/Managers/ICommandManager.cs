using AspNetCoreTelegramBot.Commands;

namespace AspNetCoreTelegramBot.Managers
{
    /// <summary>
    /// Менеджер для работы с командами
    /// </summary>
    public interface ICommandManager
    {
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
        ICommand GetCommand(string command);

        /// <summary>
        /// Проверка текста на команду
        /// </summary>
        /// <param name="command">Текст команды</param>
        /// <returns>true - команда, false - нет</returns>
        bool IsCommand(string command);
    }
}