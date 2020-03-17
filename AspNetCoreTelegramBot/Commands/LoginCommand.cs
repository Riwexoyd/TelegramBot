using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;

using Microsoft.Extensions.Configuration;

using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Команда /login
    /// </summary>
    [CommandChatType(ChatType.Private)]
    internal class LoginCommand : BotCommand
    {
        private readonly IConfiguration configuration;

        protected override ITelegramBotClient TelegramBotClient { get; }

        protected override ApplicationContext ApplicationContext { get; }

        public LoginCommand(IConfiguration configuration, ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            this.configuration = configuration;
            TelegramBotClient = telegramBotClient;
            ApplicationContext = applicationContext;
        }

        public override async Task ExecuteAsync(User sender, Chat chat)
        {
            if (string.IsNullOrEmpty(sender.Login))
            {
                sender.Login = sender.Username ?? $"user_{sender.Id}";
                await ApplicationContext.SaveChangesAsync();
            }

            var domain = configuration.GetValue<string>("DOMAIN");
            await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Ваш логин для входа: {sender.Login}.\nАдрес для входа в панель: {domain}");
        }
    }
}