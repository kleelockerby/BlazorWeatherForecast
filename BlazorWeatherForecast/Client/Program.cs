using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using System.Globalization;
using BlazorWeatherForecast.Client.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.JSInterop;

namespace BlazorWeatherForecast.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzUxNzQ4QDMxMzgyZTMzMmUzME9uNXlNcnU1d0VVblJ4SFMvWkpqZnVoRjcybVF2NE05VzNVa2hMNGkxUlk9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastServiceAPI>();

            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

            await builder.Build().RunAsync();
        }
    }
}
