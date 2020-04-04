using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Database.Extensions;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Models.Extensions;

using System;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Присоединиться к рулетке
    /// </summary>
    [CommandChatType(ChatType.Group, ChatType.Supergroup)]
    internal class JoinRouletteCommand : BotCommand
    {
        protected override ITelegramBotClient TelegramBotClient { get; }

        protected override ApplicationContext ApplicationContext { get; }

        public JoinRouletteCommand(ITelegramBotClient telegramBotClient, ApplicationContext applicationContext)
        {
            TelegramBotClient = telegramBotClient;
            ApplicationContext = applicationContext;
        }

        public override async Task ExecuteAsync(User sender, Chat chat)
        {
            var userChat = await ApplicationContext.GetUserChatAsync(sender, chat);
            if (userChat.IsRouletteParticipant)
            {
                await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"{sender.GetFullName()}, вы уже участник рулетки!");
            }
            else
            {
                userChat.IsRouletteParticipant = true;
                userChat.RouletteJoinDate = DateTime.UtcNow;
                await ApplicationContext.SaveChangesAsync();
                await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"{sender.GetFullName()} присоединился к рулетке!");
            }
        }
    }
}