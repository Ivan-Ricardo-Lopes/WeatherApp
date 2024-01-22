using IvanLopes.Forecast.Infra.Geocoding;
using IvanLopes.Forecast.Infra.Weather;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IvanLopes.Forecast.API.HealthChecks
{
    public class UsCensusGeocodingHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private UsCensusGeocodingSettings _usCensusGeocodingSettings;

        public UsCensusGeocodingHealthCheck(IHttpClientFactory httpClientFactory,
            UsCensusGeocodingSettings usCensusGeocodingSettings)
        {
            _httpClientFactory = httpClientFactory;
            _usCensusGeocodingSettings = usCensusGeocodingSettings;
        }

        public async Task<HealthCheckResult> CheckHealthAsync
        (HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient("Default"))
            {
                var response = await httpClient.GetAsync(_usCensusGeocodingSettings.ApiBaseUrl);

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
