using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    public class NickNameGeneratorInitializeService : IHostedService
    {
        INickNameGeneratorService nickNameGeneratorService;

        public NickNameGeneratorInitializeService(INickNameGeneratorService nickNameGeneratorService)
        {
            this.nickNameGeneratorService = nickNameGeneratorService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await nickNameGeneratorService.InitializeGenerator();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
