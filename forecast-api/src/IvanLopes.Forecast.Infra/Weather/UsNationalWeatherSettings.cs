namespace IvanLopes.Forecast.Infra.Weather
{
    public record UsNationalWeatherSettings
    {
        public string ApiBaseUrl { get; set; } = default!;
        public string HealthCheckUrl { get; set; } = default!;
    }
}
