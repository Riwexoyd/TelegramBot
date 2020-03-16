using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;

using System.Threading.Tasks;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Commands
{
    [CommandChatType(ChatType.Private)]
    internal class LoginCommand : Command
    {
        public LoginCommand(ITelegramBotService telegramBotService) : base(telegramBotService)
        {
        }

        public override async Task ExecuteAsync(ApplicationContext applicationContext, User sender, Chat chat)
        {
            if (!string.IsNullOrEmpty(sender.Login))
            {
                await TelegramBotService.TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Вы уже зарегистрированы.\nВаш логин: {sender.Login}.");
            }
            else
            {
                sender.Login = sender.Username ?? $"user_{sender.Id}";
                await applicationContext.SaveChangesAsync();
                await TelegramBotService.TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Вы успешно зарегистрированы.\nВаш логин для входа: {sender.Login}.");
            }
        }
    }
}