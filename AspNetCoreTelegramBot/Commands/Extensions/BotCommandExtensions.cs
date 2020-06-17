using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var className = botCommand.GetType().Name.ToLower();
            return className.EndsWith(CommandPostfix) ? className.Remove(className.Length - CommandPostfix.Length) : className;
        }
    }
}
