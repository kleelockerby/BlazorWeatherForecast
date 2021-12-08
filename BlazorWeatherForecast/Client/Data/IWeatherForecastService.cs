using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWeatherForecast.Shared;

namespace BlazorWeatherForecast.Client
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetForecastsAsync();
    }
}
