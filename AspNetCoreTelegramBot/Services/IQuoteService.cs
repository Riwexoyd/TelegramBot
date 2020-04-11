using AspNetCoreTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    public interface IQuoteService
    {
        public void Add(Quote quote);

        public void AddTagToQuote(Quote quote, string tag);

        public void AddTagToQuote(string quoteText, string tag);

        public List<Quote> GetAll();

        public void Delete(Quote quote);

    }
}
