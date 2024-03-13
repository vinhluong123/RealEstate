using System.Net.Http.Headers;
using WebAdmin.Helpers;
using WebAdmin.Models;

namespace WebAdmin.Services.Interfaces
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/WeatherForecast";

        public WeatherForecastService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");

        }
        public async Task<IEnumerable<WeatherForecastModel>> Find()
        {
            //var request = new reques
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAsync<List<WeatherForecastModel>>();
        }
    }
}
