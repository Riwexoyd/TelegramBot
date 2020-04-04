using System;
using System.Collections.Generic;

namespace AspNetCoreTelegramBot.ViewModels
{
    public class RouletteCategoryModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок категории
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Публичная категория
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Чаты
        /// </summary>
        public ICollection<ChatModel> ChatModels { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}