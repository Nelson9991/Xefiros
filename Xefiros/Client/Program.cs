using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Radzen;
using Xefiros.Client.Data.Repository;
using Xefiros.Client.Data.Repository.IRepository;
using Xefiros.Client.Helpers;
using Xefiros.Client.Services;
using Xefiros.Client.Services.IServices;

namespace Xefiros.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<HttpClientConToken>(
                    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient<HttpClientSinToken>(
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomUserFactory>();
            services.AddOptions();

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();

            services.AddScoped<IHttpRepository, HttpRepository>();
            services.AddTransient<IReadImage, ReadImage>();
            services.AddTransient<IToastNotificationService, ToastNotificationService>();
        }
    }
}