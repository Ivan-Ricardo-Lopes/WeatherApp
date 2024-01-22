
using IvanLopes.Forecast.Application.InfraServices;
using IvanLopes.Forecast.Infra.Weather;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IvanLopes.Forecast.API.HealthChecks
{
    public class UsNationalWeatherHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private UsNationalWeatherSettings _usNationalWeatherSettings;

        public UsNationalWeatherHealthCheck(IHttpClientFactory httpClientFactory,
            UsNationalWeatherSettings usNationalWeatherSettings)
        {
            _httpClientFactory = httpClientFactory;
            _usNationalWeatherSettings = usNationalWeatherSettings;
        }
        public async Task<HealthCheckResult> CheckHealthAsync
        (HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient(nameof(IWeatherService)))
            {
                var response = await httpClient.GetAsync(_usNationalWeatherSettings.ApiBaseUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: response.ToString()));
                }
                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: response.ToString()));
            }
        }
    }
}
