using AspNetCoreTelegramBot.Models;

using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Интерфейс сервиса обработчика сообщений
    /// </summary>
    public interface ITextHandlerService
    {
        Task HandleTextAsync(User sender, Chat chat, string text);
    }
}