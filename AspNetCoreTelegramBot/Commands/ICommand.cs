using AspNetCoreTelegramBot.Database;

using System.Threading.Tasks;

using Chat = AspNetCoreTelegramBot.Models.Chat;
using User = AspNetCoreTelegramBot.Models.User;

namespace AspNetCoreTelegramBot.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(ApplicationContext applicationContext, User sender, Chat chat);
    }
}