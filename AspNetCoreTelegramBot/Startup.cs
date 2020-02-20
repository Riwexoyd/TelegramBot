using System;

using AspNetCoreTelegramBot.Database;

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
            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(GetConnectionString()));

            var token = Configuration.GetValue<string>("TOKEN");

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("Invalid TOKEN");
            }

            //  регистрируем сервисы
            services.AddSingleton<ITelegramBotClient>(i => new TelegramBotClient(token));
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //  регистрируем бота

            string domain = Configuration.GetValue<string>("DOMAIN");

            if (string.IsNullOrEmpty(domain))
            {
                throw new ArgumentNullException("Invalid DOMAIN");
            }

            string hook = $"{domain}api/message/update";
            telegramBot.SetWebhookAsync(hook).Wait();
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