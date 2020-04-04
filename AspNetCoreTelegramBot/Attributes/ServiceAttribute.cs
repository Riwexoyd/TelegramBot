using Microsoft.Extensions.DependencyInjection;

using System;

namespace AspNetCoreTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут сервиса
    /// TODO: сделать автоматическую регистрацию сервисов через рефлексию
    /// </summary>
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; }

        public ServiceAttribute()
        {
            ServiceLifetime = ServiceLifetime.Scoped;
        }

        public ServiceAttribute(ServiceLifetime serviceLifetime)
        {
            ServiceLifetime = serviceLifetime;
        }
    }
}