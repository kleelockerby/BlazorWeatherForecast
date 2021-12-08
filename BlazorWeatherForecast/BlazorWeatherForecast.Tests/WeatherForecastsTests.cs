using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWeatherForecast.Client;
using BlazorWeatherForecast.Client.Pages;
using BlazorWeatherForecast.Client.Components;
using BlazorWeatherForecast.Shared;
using BlazorWeatherForecast.Client.Shared;
using Newtonsoft.Json;
using Moq;
using Bunit;
using Xunit;
using AngleSharp.Dom;
using Syncfusion.Blazor;
using Microsoft.JSInterop;

namespace BlazorWeatherForecast.Tests
{
    public class WeatherForecastsTests
    {
        [Fact]
        public void RenderFetchData()
        {
            IEnumerable<WeatherForecast> forecasts = GetWeather();
            Mock<IWeatherForecastService> weatherService = new Mock<IWeatherForecastService>();
            weatherService.Setup(x => x.GetForecastsAsync()).ReturnsAsync(forecasts);

            TestContext ctx = new TestContext();
            ctx.Services.AddSingleton(weatherService.Object);
            IRenderedComponent<FetchData> cut = ctx.RenderComponent<FetchData>();
            IRefreshableElementCollection<IElement> tableRows = cut.FindAll("tr.row-forecast");
            
            Assert.Equal(5, tableRows.Count);
        }

        [Fact]
        public void RenderMasterDetail()
        {
            IEnumerable<WeatherForecast> forecasts = GetWeather();
            Mock<IWeatherForecastService> weatherService = new Mock<IWeatherForecastService>();
            weatherService.Setup(x => x.GetForecastsAsync()).ReturnsAsync(forecasts);
            var jsrMock = new Mock<IJSRuntime>();

            TestContext ctx = new TestContext();
            ctx.Services.AddSingleton(weatherService.Object);
            ctx.Services.AddSingleton<ISyncfusionStringLocalizer>(new SyncfusionLocalizer());
            ctx.Services.AddSingleton(jsrMock.Object);
            ctx.Services.AddSyncfusionBlazor();
            
            IRenderedComponent<MasterDetail> cut = ctx.RenderComponent<MasterDetail>();
            IElement rightSymbol = cut.Find("tr.e-row td.e-detailrowcollapse[data-uid=\"gridcell-6\"]");
            rightSymbol.Click();

            var expectedForecastDetail = ctx.RenderComponent<WeatherForecastDetail>((nameof(WeatherForecastDetail.WeatherForecast), forecasts.ToList().FirstOrDefault()));
            var actualForecastDetailElement = cut.FindComponent<WeatherForecastDetail>();
            actualForecastDetailElement.MarkupMatches(expectedForecastDetail);
        }

        [Fact]
        public void CounterAdd()
        {
            TestContext ctx = new TestContext();
            IRenderedComponent<Counter> cut = ctx.RenderComponent<Counter>();

            string countValue = GetCountValue(cut);
            Assert.Equal("Current count: 0", countValue);

            cut.Find("button#btnInc").Click();
            countValue = GetCountValue(cut);

            Assert.Contains("Current count: 1", countValue);

            cut.Find("button#btnDec").Click();
            countValue = GetCountValue(cut);
            Assert.Equal("Current count: 0", countValue);
        }

        private string GetCountValue(IRenderedComponent<Counter> comp)
        {
            IElement element = comp.Find("#count");
            string countValue = element.GetInnerText();
            return countValue;
        }

        private IEnumerable<WeatherForecast> GetWeather()
        {
            using (StreamReader r = new StreamReader(@"C:\SourceFiles\weatherForecast.json"))
            {
                string json = r.ReadToEnd();
                IEnumerable<WeatherForecast> forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(json);
                return forecasts;
            }
        }
    }
}
