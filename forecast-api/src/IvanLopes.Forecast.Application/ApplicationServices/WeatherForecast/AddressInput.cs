namespace IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast
{
    public record AddressInput
    {
        public string Street { get; set; } = default!;
        public string? City { get; set; }
        public string? StateCode { get; set; }
        public string? ZipCode { get; set; }
    }

}
