using AspNetCoreTelegramBot.Commands;
using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AspNetCoreTelegramBot.Managers
{
    internal class CommandManager : ICommandManager
    {
        private const string CommandPostfix = "command";
        private readonly Dictionary<string, ICommand> commands;
        private readonly ITelegramBotService telegramBotService;
        private readonly Regex commandRegex;

        public CommandManager(ITelegramBotService telegramBotService)
        {
            this.telegramBotService = telegramBotService;
            var commands = ReflectionHelper.GetImplimentationTypes(typeof(ICommand))
                .Select(i => Activator.CreateInstance(i, telegramBotService) as ICommand);
            this.commands = commands.ToDictionary(i =>
            {
                string name = i.GetType().Name.ToLower();
                name = name.EndsWith(CommandPostfix) ? name.Remove(name.Length - CommandPostfix.Length) : name;
                Console.WriteLine($"Command: {name}");
                return name;
            },
            j => j);

            commandRegex = new Regex($@"(^(\/)[a-z]+@{telegramBotService.BotUser.Username})|(^(\/)[a-z]*$)");

            //  сюда тоже логгер

            Console.WriteLine($"Commands count: {commands.Count()}");
        }

        public bool ContainsCommand(string command)
        {
            return commands.ContainsKey(ParseCommand(command));
        }

        public ICommand GetCommand(string command)
        {
            if (!ContainsCommand(command))
            {
                throw new ArgumentException("Invalid command");
            }

            return commands[ParseCommand(command)];
        }

        private string ParseCommand(string command)
        {
            return command.Split('@').First().Substring(1);
        }

        public bool IsCommand(string command)
        {
            return commandRegex.IsMatch(command);
        }
    }
}