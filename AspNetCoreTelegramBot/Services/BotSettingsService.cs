using AspNetCoreTelegramBot.Commands;
using AspNetCoreTelegramBot.Commands.Extensions;
using AspNetCoreTelegramBot.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис для установки настроек телеграм бота
    /// </summary>
    public class BotSettingsService : IHostedService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<BotSettingsService> logger;
        private readonly ITelegramBotClient telegramBotClient;

        public BotSettingsService(ITelegramBotClient telegramBotClient,
            IConfiguration configuration,
            ILogger<BotSettingsService> logger)
        {
            this.telegramBotClient = telegramBotClient;
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// Установить WebHook для бота
        /// </summary>
        /// <returns>Task</returns>
        private async Task SetWebHookAsync()
        {
            //  регистрируем бота и устанавливаем webhook
            logger.LogInformation("Setting webhooks");
            var domain = configuration.GetValue<string>("DOMAIN");
            ExceptionHelper.ThrowIfNullOrEmpty(domain, "DOMAIN");

            string hook = $"{domain}/api/message";
            await telegramBotClient.SetWebhookAsync(hook);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SetWebHookAsync();
            await UpdateCommandsAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await telegramBotClient.DeleteWebhookAsync();
        }

        /// <summary>
        /// Обновить команды бота
        /// </summary>
        /// <returns>Task</returns>
        private async Task UpdateCommandsAsync()
        {
            List<BotCommand> commands = new List<BotCommand>();
            var botCommands = ReflectionHelper.GetImplimentationTypes(typeof(IBotCommand));
            foreach (var botCommand in botCommands)
            {
                var command = new BotCommand()
                {
                    Command = botCommand.GetTypeNameAsCommand(),
                    Description = botCommand.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "Описание отсутствует"
                };
                commands.Add(command);
            }
            await telegramBotClient.SetMyCommandsAsync(commands);
        }
    }
}