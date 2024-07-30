namespace LightHouseScanner
{
    using Blazor.Analytics;
    using LightHouseScanner.Services;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net.Http.Json;
    using Toolbelt.Blazor.Extensions.DependencyInjection;
    using static System.Net.WebRequestMethods;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddLoadingBar(options =>
            {
                options.LoadingBarColor = "blue";
                //options.LatencyThreshold = 3000;
                options.LatencyThreshold = 500;
                options.ContainerSelector = "#selector-of-container";
            });

            var http = new HttpClient()
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };

            builder.Services.AddScoped(sp => http);

            await ConfigureServices(builder, http);

            builder.Services.AddHttpClient("DefaultHttpClient", (sp, client) =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                client.EnableIntercept(sp);
            });

            builder.Services.AddHttpClient("ApiHttpClient", (sp, client) =>
            {
                client.BaseAddress = new Uri("https://website-lighthouse-seo-scanner-online.niraiyaemailaccounts.workers.dev/");
                client.Timeout = TimeSpan.FromSeconds(60);
                client.EnableIntercept(sp);
            });

            builder.Services.AddScoped<ApiService>();

            builder.UseLoadingBar();
            builder.Services.AddGoogleAnalytics("G-NWPMMWZY48");
            await builder.Build().RunAsync();
        }

        public static async Task ConfigureServices(WebAssemblyHostBuilder builder, HttpClient http)
        {
            string appSettingsFile = string.Empty;

#if DEBUG
            appSettingsFile = "appsettings.json";
#else
            appSettingsFile = $"appsettings.Production.json";
            builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif
            using var response = await http.GetAsync(appSettingsFile);
            using var stream = await response.Content.ReadAsStreamAsync();

            builder.Configuration.AddJsonStream(stream);
        }
    }
}