using System.Threading.Tasks;
using AspNetCoreTelegramBot.Database;
using Microsoft.AspNetCore.Mvc;

using Telegram.Bot.Types;

namespace AspNetCoreTelegramBot.Controllers
{
    /// <summary>
    /// Контроллер сообщений для бота
    /// </summary>
    //[ApiController]
    [Route("api/message/update")]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;

        public MessageController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Method GET unuvalable";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            return Ok();
        }
    }
}