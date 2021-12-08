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
using Newtonsoft.Json;
using Moq;
using Bunit;
using Xunit;
using AngleSharp.Dom;

namespace BlazorWeatherForecast.Tests
{
    public class CounterTests
    {
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
    }
}
