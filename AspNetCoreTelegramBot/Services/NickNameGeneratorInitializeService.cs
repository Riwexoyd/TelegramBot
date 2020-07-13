using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    public class NickNameGeneratorInitializeService : IHostedService
    {
        private readonly INickNameGeneratorService nickNameGeneratorService;

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