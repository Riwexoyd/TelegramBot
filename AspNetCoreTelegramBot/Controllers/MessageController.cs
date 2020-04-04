using AspNetCoreTelegramBot.Services;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using Telegram.Bot.Types;

namespace AspNetCoreTelegramBot.Controllers
{
    /// <summary>
    /// Контроллер сообщений для бота
    /// </summary>
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        /// <summary>
        /// Сервис обновлений
        /// </summary>
        private readonly IUpdateService updateService;

        public MessageController(IUpdateService updateService)
        {
            this.updateService = updateService;
        }

        /// <summary>
        /// GET api/message/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Method GET unavailable";
        }

        /// <summary>
        /// POST api/message/
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await updateService.HandleUpdateAsync(update);
            return Ok();
        }
    }
}