using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;
using AspNetCoreTelegramBot.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Telegram.Bot.Types.Enums;

namespace AspNetCoreTelegramBot.Controllers
{
    /// <summary>
    /// Контроллер аккаунта
    /// https://metanit.com/sharp/aspnet5/15.2.php
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly ITelegramBotService telegramBotService;
        private readonly IAuthService authService;

        public AccountController(ApplicationContext applicationContext, ITelegramBotService telegramBotService, IAuthService authService)
        {
            this.applicationContext = applicationContext;
            this.telegramBotService = telegramBotService;
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SendCode(string login)
        {
            var user = await applicationContext.Users.FirstOrDefaultAsync(i => i.Login == login);
            if (user == null)
            {
                return Json(false);
            }

            var code = authService.GenerateCode(user);
            var chat = await applicationContext.Chats.FirstOrDefaultAsync(i => i.TelegramChatType == ChatType.Private && i.TelegramId == user.TelegramId);

            if (chat == null)
            {
                return Json(false);
            }

            await telegramBotService.TelegramBotClient.SendTextMessageAsync(chat.TelegramId, $"Пароль для входа в панель управления: {code}.");

            return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await applicationContext.Users.FirstOrDefaultAsync(i => i.Login == model.Login);
                if (user != null)
                {
                    if (authService.IsCorrectCode(user, model.Code))
                    {
                        await Authenticate(model.Login); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректный код аутентификации");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Некорректный логин");
                }
            }
            return View(model);
        }

        private async Task Authenticate(string login)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}