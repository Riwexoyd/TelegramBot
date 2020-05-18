using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    /// <summary>
    /// Сервис для получения случайного гуся
    /// </summary>
    public interface IGooseService
    {
        /// <summary>
        /// Получить случайного гуся
        /// </summary>
        /// <returns>Строка с гусём</returns>
        string GetRandomGoose();
    }
}
