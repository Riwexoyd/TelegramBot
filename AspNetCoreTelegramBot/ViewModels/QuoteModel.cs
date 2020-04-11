using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.ViewModels
{
    public class QuoteModel
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public List<string> Tags { get; set; }
    }
}
