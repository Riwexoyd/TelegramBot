using AspNetCoreTelegramBot.Attributes;
using AspNetCoreTelegramBot.Commands;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Database.Extensions;
using AspNetCoreTelegramBot.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using DbChat = AspNetCoreTelegramBot.Models.Chat;

namespace AspNetCoreTelegramBot.Controllers
{
    /// <summary>
    /// Контроллер сообщений для бота
    /// </summary>
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly ITelegramBotService telegramBotService;

        public MessageController(ApplicationContext applicationContext, ITelegramBotService telegramBotService)
        {
            this.applicationContext = applicationContext;
            this.telegramBotService = telegramBotService;
        }

        /// <summary>
        /// GET api/message/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Method GET unuvalable";
        }

        /// <summary>
        /// POST api/message/
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update != null)
            {
                Func<Task> execute = update.Type switch
                {
                    UpdateType.Message => () => CheckMessage(update.Message),
                    _ => () => Task.FromException(new NotSupportedException())
                };

                try
                {
                    await execute();
                }
                catch (Exception e)
                {
                    //  TODO: сюда логгер
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Update IS NULL");
            }

            return Ok();
        }

        private async Task CheckMessage(Message message)
        {
            if (message != null)
            {
                var user = await applicationContext.GetUserFromTelegramModel(message.From);
                var chat = await applicationContext.GetChatFromTelegramModel(message.Chat);
                if (telegramBotService.CommandManager.IsCommand(message.Text))
                {
                    if (telegramBotService.CommandManager.ContainsCommand(message.Text))
                    {
                        var command = telegramBotService.CommandManager.GetCommand(message.Text);
                        if (CanExecuteCommand(command, chat, out string errorMessage))
                        {
                            await command.ExecuteAsync(applicationContext, user, chat);
                        }
                        else
                        {
                            await telegramBotService.TelegramBotClient.SendTextMessageAsync(chat.TelegramId, errorMessage);
                            throw new OperationCanceledException(errorMessage);
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Unknown command: {message.Text}");
                    }
                }
                else
                {
                    //  Todo: обработчики текста
                    throw new NotSupportedException("No text handler implementation");
                }
            }
            else
            {
                throw new ArgumentException("Message is null");
            }
        }

        private bool CanExecuteCommand(ICommand command, DbChat chat, out string errorMessage)
        {
            var attributes = command.GetType().GetCustomAttributes(false);
            foreach (CommandChatTypeAttribute commandChatType in attributes)
            {
                if (commandChatType.ChatType != chat.TelegramChatType)
                {
                    errorMessage = "Данную команду нельзя использовать в данном чате";
                    return false;
                }
                else
                {
                    //  ничего не делаем
                }
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}