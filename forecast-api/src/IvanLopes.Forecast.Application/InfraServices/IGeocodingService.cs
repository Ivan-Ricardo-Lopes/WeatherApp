using IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast;
using IvanLopes.Forecast.Application.Dtos;

namespace IvanLopes.Forecast.Application.InfraServices
{
    public interface IGeocodingService
    {
        Task<CoordinatesDto?> GetCoordinatesAsync(string street, string? city = null, string? stateCode = null, string? zipCode = null);
    }
}
