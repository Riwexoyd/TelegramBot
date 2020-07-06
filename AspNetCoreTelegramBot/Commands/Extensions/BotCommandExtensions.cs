using System;

namespace AspNetCoreTelegramBot.Commands.Extensions
{
    /// <summary>
    /// Расширения для интерфейса BotCommand
    /// </summary>
    public static class BotCommandExtensions
    {
        private const string CommandPostfix = "command";

        /// <summary>
        /// Получить имя команды по объекту класса
        /// </summary>
        /// <param name="botCommand">Команда бота</param>
        /// <returns>Имя команды</returns>
        public static string GetCommandName(this IBotCommand botCommand)
        {
            return botCommand.GetType().GetTypeNameAsCommand();
        }

        /// <summary>
        /// Получить имя типа как команду
        /// </summary>
        /// <param name="botCommandType">Тип команды</param>
        /// <returns>Имя типа в формате команды</returns>
        public static string GetTypeNameAsCommand(this Type botCommandType)
        {
            var className = botCommandType.Name.ToLower();
            return className.EndsWith(CommandPostfix) ? className.Remove(className.Length - CommandPostfix.Length) : className;
        }
    }
}