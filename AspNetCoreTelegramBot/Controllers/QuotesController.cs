using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTelegramBot.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTelegramBot.Controllers
{
    public class QuotesController : Controller
    {
        public IActionResult Index()
        {

            List<QuoteModel> quotes = new List<QuoteModel>();

            QuoteModel testQuote = new QuoteModel()
            {
                Id = 1,
                Text = "test",
                Tags = new List<string>()
                {
                    "tag1", "tag2"
                }
            };

            quotes.Add(testQuote);

            ViewBag.Quotes = quotes;
            
            return View();
        }

        [HttpPost]
        public IActionResult AddQuote(QuoteModel model)
        {
            return null;
        }
        
        [HttpPost]
        public IActionResult AddTag(string tag)
        {
            return null;
        }

        public IActionResult DeleteQuote(dynamic body)
        {
            return null;
        }

    }
}