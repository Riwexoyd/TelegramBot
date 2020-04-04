using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.CallbackQueries;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Database.Extensions;
using AspNetCoreTelegramBot.Enums;
using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Models.Extensions;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

using User = AspNetCoreTelegramBot.Models.User;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис обработки запросов
    /// </summary>
    internal class CallbackQueryService : ICallbackQueryService
    {
        /// <summary>
        /// Ключ типа запроса
        /// </summary>
        private const string QueryTypeKey = "Query";

        /// <summary>
        /// Отправитель запроса
        /// </summary>
        private const string QuerySender = "Sender";

        private readonly ApplicationContext applicationContext;
        private readonly ITelegramBotClient telegramBotClient;
        private readonly IEnumerable<ICallbackQuery> callbackQueries;
        private readonly ILogger<ICallbackQueryService> logger;

        public CallbackQueryService(ApplicationContext applicationContext,
            ITelegramBotClient telegramBotClient,
            IEnumerable<ICallbackQuery> callbackQueries,
            ILogger<ICallbackQueryService> logger)
        {
            this.applicationContext = applicationContext;
            this.telegramBotClient = telegramBotClient;
            this.callbackQueries = callbackQueries;
            this.logger = logger;
        }

        public string CreateQuery<T>(User user, params (string key, object value)[] args) where T : ICallbackQuery
        {
            var data = args.ToDictionary(i => i.key, j => j.value);
            data.Add(QueryTypeKey, typeof(T).Name);

            var attribute = GetCallbackQueryTypeAttribute(typeof(T));

            if (attribute != null)
            {
                switch (attribute.CallbackQueryType)
                {
                    case CallbackQueryType.Public:  //  ничего не делаем
                        break;

                    case CallbackQueryType.SenderOnly:
                        data.Add(QuerySender, user.Id);
                        break;

                    default:
                        throw new NotSupportedException($"Unknown CallbackQueryType: {attribute.CallbackQueryType}");
                }
            }

            var serialize = JsonConvert.SerializeObject(data);
            return serialize;
        }

        /// <summary>
        /// Обработать запрос асинхронно
        /// </summary>
        /// <param name="callbackQuery">Callback запрос</param>
        /// <returns></returns>
        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            ExceptionHelper.ThrowIfNull(callbackQuery, "callbackQuery");
            ExceptionHelper.ThrowIfNull(callbackQuery.From, "callbackQuery.From");
            ExceptionHelper.ThrowIfNull(callbackQuery.Message, "callbackQuery.Message");
            ExceptionHelper.ThrowIfNull(callbackQuery.Message.Chat, "callbackQuery.Message.Chat");
            ExceptionHelper.ThrowIfNullOrEmpty(callbackQuery.Data, "callbackQuery.Data");

            //  отвечаем получение
            await telegramBotClient.AnswerCallbackQueryAsync(callbackQuery.Id);

            //  получаем отправителя и чат
            var user = await applicationContext.GetUserFromTelegramModel(callbackQuery.From);
            var chat = await applicationContext.GetChatFromTelegramModel(callbackQuery.Message.Chat);
            //  парсим данные запросы
            var data = ParseQueryData(callbackQuery.Data);
            //  получаем запрос
            var query = GetCallbackQuery(data);
            //  если запрос неизвестный, то бросаем исключение
            ExceptionHelper.ThrowIfNull(query, "query");

            if (CanExecuteQuery(query, user, data))
            {
                await query.ExecuteQueryAsync(user, chat, data);
            }
            else
            {
                await telegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Невозможно обработать нажатие кнопки от пользователя {user.GetFullName()}");
                logger.LogInformation($"Unable to execute query {query} from user.Id {user.Id}");
            }
        }

        /// <summary>
        /// Получить объект типа запроса
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>CallBack query</returns>
        private ICallbackQuery GetCallbackQuery(Dictionary<string, object> data)
        {
            ExceptionHelper.ThrowIfNotContains(data, "data", QueryTypeKey);
            var type = data[QueryTypeKey].ToString();
            return callbackQueries.FirstOrDefault(i => i.GetType().Name == type);
        }

        private Dictionary<string, object> ParseQueryData(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
        }

        /// <summary>
        /// Проверить возможность выполнения запроса
        /// </summary>
        /// <param name="callbackQuery">Запрос</param>
        /// <param name="sender">Отправитель запроса</param>
        /// <param name="data">Данные запроса</param>
        /// <returns>True, если можно выполнить; Иначе False</returns>
        private bool CanExecuteQuery(ICallbackQuery callbackQuery, User sender, Dictionary<string, object> data)
        {
            var attribute = GetCallbackQueryTypeAttribute(callbackQuery.GetType());

            if (attribute != null)
            {
                bool SenderHasSameIdAsQueryCreator()
                {
                    return data.ContainsKey(QuerySender)
                            && int.TryParse(data[QuerySender].ToString(), out int val)
                            && val == sender.Id;
                }

                var result = attribute.CallbackQueryType switch
                {
                    CallbackQueryType.Public => true,
                    CallbackQueryType.SenderOnly => SenderHasSameIdAsQueryCreator(),
                    _ => throw new NotImplementedException($"Unknown CallbackQueryType: {attribute.CallbackQueryType}"),
                };

                return result;
            }

            return true;
        }

        private static CallbackQueryTypeAttribute GetCallbackQueryTypeAttribute(Type type)
        {
            var attribute = type.GetCustomAttributes(false)
                                .OfType<CallbackQueryTypeAttribute>()
                                .FirstOrDefault();

            return attribute;
        }
    }
}