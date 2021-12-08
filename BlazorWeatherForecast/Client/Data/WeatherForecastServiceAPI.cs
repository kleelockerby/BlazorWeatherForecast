using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWeatherForecast.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorWeatherForecast.Client
{
    public class WeatherForecastServiceAPI : IWeatherForecastService
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastServiceAPI(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public Task<IEnumerable<WeatherForecast>> GetForecastsAsync()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("WeatherForecast");
        }
    }
}
