using AspNetCoreTelegramBot.CallbackQueries;
using AspNetCoreTelegramBot.Commands;
using AspNetCoreTelegramBot.Database;
using AspNetCoreTelegramBot.Helpers;
using AspNetCoreTelegramBot.Services;
using AspNetCoreTelegramBot.TextHandlers;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Telegram.Bot;

namespace AspNetCoreTelegramBot
{
    /// <summary>
    /// Стартап
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            //  добавляем контроллеры и вью
            services.AddControllersWithViews()
                .AddNewtonsoftJson();

            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(GetConnectionString()));

            //  регистрируем сервисы
            var token = Configuration.GetValue<string>("TOKEN");
            ExceptionHelper.ThrowIfNullOrEmpty(token, "TOKEN");
            services.AddSingleton<ITelegramBotClient>(i => new TelegramBotClient(token));
            services.AddHostedService<BotSettingsService>();

            services.AddTransient<ITextHandlerService, TextHandlerService>();
            services.AddTransient<ICommandService, CommandService>();
            services.AddTransient<IUpdateService, UpdateService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ICallbackQueryService, CallbackQueryService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IGooseService, GooseService>();

            RegisterAsTransient<IBotCommand>(services);
            RegisterAsTransient<ITextHandler>(services);
            RegisterAsTransient<ICallbackQuery>(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }

        /// <summary>
        /// Зарегистрировать реализации типа как Transient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        private void RegisterAsTransient<T>(IServiceCollection serviceCollection)
        {
            var types = ReflectionHelper.GetImplimentationTypes(typeof(T));
            foreach (var type in types)
            {
                serviceCollection.AddTransient(typeof(T), type);
            }
        }

        private string GetConnectionString()
        {
            var connectionUrl = Configuration.GetValue<string>("DATABASE_URL");
            ExceptionHelper.ThrowIfNullOrEmpty(connectionUrl, "DATABASE_URL");

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