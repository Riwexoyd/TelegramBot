using AspNetCoreTelegramBot.Enums;

using System;

namespace AspNetCoreTelegramBot.Attributes
{
    /// <summary>
    /// Атрибут типа Callback Query
    /// </summary>
    public class CallbackQueryTypeAttribute : Attribute
    {
        /// <summary>
        /// Тип Callback Query
        /// </summary>
        public CallbackQueryType CallbackQueryType { get; set; }

        public CallbackQueryTypeAttribute(CallbackQueryType callbackQueryType)
        {
            CallbackQueryType = callbackQueryType;
        }

        public CallbackQueryTypeAttribute()
        {
            CallbackQueryType = CallbackQueryType.Public;
        }
    }
}