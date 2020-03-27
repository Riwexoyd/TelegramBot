using System.Threading.Tasks;

using Chat = AspNetCoreTelegramBot.Models.Chat;
using User = AspNetCoreTelegramBot.Models.User;

namespace AspNetCoreTelegramBot.Commands
{
    public interface IBotCommand
    {
        Task ExecuteAsync(User sender, Chat chat);
    }
}