using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MihaZupan;

using System;

using Telegram.Bot;

namespace AspNetCoreTelegramBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            services.AddControllersWithViews()
                .AddNewtonsoftJson();

            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(GetConnectionString()));

            var token = Configuration.GetValue<string>("TOKEN");

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("Invalid TOKEN");
            }

            //  регистрируем сервисы
#if DEBUG
            //  TODO: настроить ngrok
            services.AddSingleton<ITelegramBotClient>(i => new TelegramBotClient(token, new HttpToSocks5Proxy("127.0.0.1", 9050)));
#else
            services.AddSingleton<ITelegramBotClient>(i => new TelegramBotClient(token));
#endif
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.AddSingleton<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITelegramBotClient telegramBot)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();    // аутентификация
            app.UseAuthorization();     // авторизация

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
#if RELEASE

            //  регистрируем бота и устанавливаем webhook
            //  TODO: настроить ngrok
            var domain = Configuration.GetValue<string>("DOMAIN");
            if (string.IsNullOrEmpty(domain))
            {
                throw new ArgumentNullException("Invalid DOMAIN");
            }

            string hook = $"{domain}/api/message";
            telegramBot.SetWebhookAsync(hook).Wait();
#endif
        }

        public string GetConnectionString()
        {
            var connectionUrl = Configuration.GetValue<string>("DATABASE_URL");
            if (string.IsNullOrEmpty(connectionUrl))
            {
                throw new ArgumentNullException("Invalid DATABASE_URL");
            }

            // Parse connection URL to connection string for Npgsql
            connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
            var pgUserPass = connectionUrl.Split("@")[0];
            var pgHostPortDb = connectionUrl.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            return $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};sslmode=Prefer;Trust Server Certificate=true";
        }
    }
}