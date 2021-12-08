using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWeatherForecast.Shared;

namespace BlazorWeatherForecast.Client
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IEnumerable<WeatherForecast>> GetForecastsAsync()
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(3, 7).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }));
        }
    }
}
