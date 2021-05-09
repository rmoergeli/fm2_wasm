using fm2_wasm.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace fm2_wasm.Client.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        #region Properties
        private readonly ILogger<WeatherViewModel> _logger;
        private HttpClient _http;

        private WeatherForecast[] weatherForecasts;
        public WeatherForecast[] WeatherForecasts
        {
            get => weatherForecasts;
            private set
            {
                weatherForecasts = value;
                OnPropertyChanged(nameof(WeatherForecasts));
            }
        }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Constructor(s)
        /// </summary>
        public WeatherViewModel(ILogger<WeatherViewModel> logger, HttpClient http)
        {
            Trace.WriteLine("Constructor...");

            _logger = logger;
            _http = http;

            _ = RetrieveForecasts();
        }
        #endregion

        private async Task RetrieveForecasts()
        {
            WeatherForecasts = await RetrieveForecastsAsync();
        }

        private async Task<WeatherForecast[]> RetrieveForecastsAsync()
        {
            WeatherForecast[] _weatherForecast = await _http.GetFromJsonAsync<WeatherForecast[]>("api/SampleData/WeatherForecasts");

            return _weatherForecast;
        }
    }
}
