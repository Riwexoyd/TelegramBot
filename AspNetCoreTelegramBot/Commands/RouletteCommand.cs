using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.CallbackQueries;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AspNetCoreTelegramBot.Commands
{
    /// <summary>
    /// Команда для рулетки
    /// </summary>
    [CommandChatType(ChatType.Group, ChatType.Supergroup)]
    [Description("Запустить рулетку")]
    internal class RouletteCommand : IBotCommand
    {
        private readonly ICallbackQueryService callbackQueryService;
        protected ITelegramBotClient TelegramBotClient { get; }
        protected ApplicationContext ApplicationContext { get; }

        public RouletteCommand(ICallbackQueryService callbackQueryService,
            ITelegramBotClient telegramBotClient,
            ApplicationContext applicationContext)
        {
            this.callbackQueryService = callbackQueryService;
            TelegramBotClient = telegramBotClient;
            ApplicationContext = applicationContext;
        }

        public async Task ExecuteAsync(User sender, Chat chat)
        {
            var categories = await ApplicationContext.RouletteCategories
                .Where(i => i.IsPublic || i.RouletteCategoryChats.Any(j => j.Chat == chat))
                .ToListAsync();

            if (!categories.Any())
            {
                await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, "Нет категорий для выбора");
            }
            else
            {
                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(categories.Select(i => new InlineKeyboardButton[]
                {
                    new InlineKeyboardButton()
                    {
                        Text = i.Title,
                        CallbackData = callbackQueryService.CreateQuery<RouletteQuery>(sender, ("Id", i.Id))
                    }
                }
                .ToArray()));

                await TelegramBotClient.SendTextMessageAsync(chat.TelegramId, "Выберите категорию:", replyMarkup: inlineKeyboardMarkup);
            }
        }
    }
}