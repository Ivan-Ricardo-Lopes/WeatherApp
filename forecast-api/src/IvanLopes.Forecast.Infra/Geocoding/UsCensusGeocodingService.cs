using IvanLopes.Forecast.Application.Dtos;
using IvanLopes.Forecast.Application.InfraServices;
using IvanLopes.Forecast.Infra.Weather.Geocoding.OneLineAddressResponseModels;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace IvanLopes.Forecast.Infra.Geocoding
{
    public class UsCensusGeocodingService : IGeocodingService
    {
        private readonly UsCensusGeocodingSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsCensusGeocodingService> _logger;

        public UsCensusGeocodingService(UsCensusGeocodingSettings settings,
            IHttpClientFactory httpClientFactory,
            ILogger<UsCensusGeocodingService> logger)
        {
            this._settings = settings;
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }

        public async Task<CoordinatesDto?> GetCoordinatesAsync(string street, string? city = null, string? stateCode = null, string? zipCode = null)
        {
            if (string.IsNullOrEmpty(street?.Trim()))
            {
                throw new ArgumentException("Address cannot be null or whitespace.", nameof(street));
            }

            HttpClient client = _httpClientFactory.CreateClient("Default");
            var baseUrl = _settings.ApiBaseUrl;
            var urlBuilder = new StringBuilder($"{baseUrl}/geocoder/locations/address?street={street}&");

            if (!string.IsNullOrEmpty(city))
            {
                urlBuilder.Append($"city={city}&");
            }

            if (!string.IsNullOrEmpty(stateCode))
            {
                urlBuilder.Append($"state={stateCode}&");
            }

            if (!string.IsNullOrEmpty(zipCode))
            {
                urlBuilder.Append($"zip={zipCode}&");
            }

            urlBuilder.Append("benchmark=Public_AR_Current&format=json");

            var url = urlBuilder.ToString();
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error getting coordinates from UsCensusGeocodingService. Status code: {StatusCode}", response.StatusCode);
                return null;
            }

            string content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                _logger.LogError("Error getting coordinates from UsCensusGeocodingService. Content is null or whitespace.");
                return null;
            }

            try
            {
                var oneLineAddressResponse = JsonSerializer.Deserialize<OneLineAddressResponse>(content);

                var coordinates = oneLineAddressResponse?.Result?.AddressMatches?.FirstOrDefault()?.Coordinates;
                if (coordinates == null)
                {
                    return null;
                }

                return new CoordinatesDto(coordinates.X, coordinates.Y);
            }
            catch (JsonException e)
            {
                throw new Exception("Error deserializing response from UsCensusGeocodingService", e);
            }
        }
    }
}
