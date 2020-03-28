using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Enums;
using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Models.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;

namespace AspNetCoreTelegramBot.CallbackQueries
{
    /// <summary>
    /// Запрос для рулетки
    /// </summary>
    [CallbackQueryType(CallbackQueryType.SenderOnly)]
    internal class RouletteQuery : ICallbackQuery
    {
        private readonly ApplicationContext applicationContext;
        private readonly ITelegramBotClient telegramBotClient;
        private readonly ILogger<RouletteQuery> logger;

        public RouletteQuery(ApplicationContext applicationContext, ITelegramBotClient telegramBotClient, ILogger<RouletteQuery> logger)
        {
            this.applicationContext = applicationContext;
            this.telegramBotClient = telegramBotClient;
            this.logger = logger;
        }

        public async Task ExecuteQueryAsync(User sender, Chat chat, Dictionary<string, object> data)
        {
            ExceptionHelper.ThrowIfNotContains(data, "data", "Id");
            var id = data["Id"].ToString();
            ExceptionHelper.ThrowIfNullOrEmpty(id, "Id");
            var caterogyId = int.Parse(id);
            var category = await applicationContext.RouletteCategories.FirstOrDefaultAsync(i => i.Id == caterogyId);

            if (category == null)
            {
                await telegramBotClient.SendTextMessageAsync(chat.TelegramId, "Неизвестная категория");
                logger.LogWarning($"Unknown category: {category.Id}");
            }
            else
            {
                var permission = await HasPermission(category, chat);
                if (!permission)
                {
                    await telegramBotClient.SendTextMessageAsync(chat.TelegramId, "Нет доступа к категории");
                    logger.LogWarning($"No permission for category: {category.Id}");
                }
                else
                {
                    var rouletteCategoryChat = await applicationContext.RouletteCategoryChats.FirstOrDefaultAsync(i => i.Chat == chat && i.RouletteCategory == category);

                    if (rouletteCategoryChat == null)
                    {
                        rouletteCategoryChat = new RouletteCategoryChat
                        {
                            Chat = chat,
                            RouletteCategory = category
                        };

                        applicationContext.RouletteCategoryChats.Add(rouletteCategoryChat);
                        await applicationContext.SaveChangesAsync();
                    }

                    var winner = await applicationContext.RouletteWinners
                        .Where(i => i.RouletteCategoryChat == rouletteCategoryChat && i.Date.Date == DateTime.UtcNow.Date)
                        .Select(i => i.User)
                        .FirstOrDefaultAsync();

                    if (winner != null)
                    {
                        await telegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Сегодняшним победителем в категории \"{category.Title}\" является {winner.GetFullName()}");
                    }
                    else
                    {
                        var users = await applicationContext.Users.Where(i => i.UserChats.Any(i => i.Chat == chat && i.IsRouletteParticipant))
                            .ToListAsync();

                        if (!users.Any())
                        {
                            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, "Нет участников рулетки в данном чате!");
                        }
                        else
                        if (users.Count < 2)
                        {
                            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, "Слишком мало участников для запуска рулетки. Требуется минимум 2 пользователя.");
                        }
                        else
                        {
                            Random random = new Random();
                            var winnerNumber = random.Next(users.Count);
                            winner = users[winnerNumber];

                            rouletteCategoryChat.Winners.Add(new RouletteWinner
                            {
                                User = winner,
                                RouletteCategoryChat = rouletteCategoryChat
                            });
                            await applicationContext.SaveChangesAsync();

                            await telegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Поздравляем {winner.GetFullName()} с победой в категории \"{category.Title}\"");
                        }
                    }
                }
            }
        }

        private async Task<bool> HasPermission(RouletteCategory category, Chat chat)
        {
            return await (category.IsPublic ? Task.FromResult(true) : applicationContext.RouletteCategoryChats
                .AnyAsync(i => i.ChatId == chat.Id && i.RouletteCategoryId == category.Id));
        }
    }
}