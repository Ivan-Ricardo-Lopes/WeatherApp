namespace IvanLopes.Forecast.Infra.Geocoding
{
    public record UsCensusGeocodingSettings
    {
        public string ApiBaseUrl { get; set; } = default!;
        public string HealthCheckUrl { get; set; } = default!;
    }
}
