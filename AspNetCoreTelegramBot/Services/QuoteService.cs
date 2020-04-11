using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly ApplicationContext applicationContext;

        public async void Add(Quote quote)
        {
            await applicationContext.Quotes.AddAsync(new Quote()
            {
                Author = null,
                CreationDate = DateTime.Now,
                Text = quote.Text,
                QuoteKeywords = null,
            });

        }

        public void AddTagToQuote(Quote quote, string tag)
        {
            throw new NotImplementedException();
        }

        public async void AddTagToQuote(string quoteText, string tag)
        {
            //var quote = await applicationContext.Quotes.Where(it => it.Text == quoteText);
        }

        public void Delete(Quote quote)
        {
            
            throw new NotImplementedException();
        }

        public List<Quote> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
