namespace LightHouseScanner
{
    using Blazor.Analytics;
    using LightHouseScanner.Services;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Toolbelt.Blazor.Extensions.DependencyInjection;

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
                options.LatencyThreshold = 50;
                options.ContainerSelector = "#selector-of-container";
            });

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
    }
}