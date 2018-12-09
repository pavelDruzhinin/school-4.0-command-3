using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Auctionator.Data;
using Auctionator.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Auctionator.Hubs;
using Microsoft.AspNetCore.Http.Connections;
using Auctionator.Services.Interface;
using Auctionator.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Auctionator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Регистрация сервисов
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IUserService, UserService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Подключение сервиса аутентификации
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.Cookie.MaxAge = TimeSpan.FromDays(30);
                    options.LoginPath = new PathString("/login");
                    options.LogoutPath = new PathString("/logout");
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR().AddHubOptions<AuctionHub>(hubOptions => // только для Аукциона
            {
                hubOptions.EnableDetailedErrors = false;
                hubOptions.KeepAliveInterval = System.TimeSpan.FromMinutes(1);
                hubOptions.HandshakeTimeout = System.TimeSpan.FromMinutes(5); // таймаут 5 минут для бездействия пользователя
            });



            // Добавление своих настроек
            //var webServiceUri = Configuration.GetSection("ConnectionStrings");
            //services.Configure<Settings.WebService>(webServiceUri);
            //// По аналогии можно добавлять настройки из своих секций, созданных в appsettings.json.
            //// Название элемента секции должно совпадать с именем свойства!!!
            //var auctionSettings = Configuration.GetSection("AuctionSettings");
            //services.Configure<Settings.Auction>(auctionSettings);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebpackDevMiddleware(
                    new WebpackDevMiddlewareOptions
                    {
                        HotModuleReplacement = true
                    });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<AuctionHub>("/auctionHub", options =>
                {
                    options.ApplicationMaxBufferSize = 64;
                    options.TransportMaxBufferSize = 64;
                    options.LongPolling.PollTimeout = System.TimeSpan.FromMinutes(1);
                    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute(name: "spa-fallback", defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
