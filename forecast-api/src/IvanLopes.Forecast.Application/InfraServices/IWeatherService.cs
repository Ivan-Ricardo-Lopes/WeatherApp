using IvanLopes.Forecast.Application.Dtos;

namespace IvanLopes.Forecast.Application.InfraServices
{
    public interface IWeatherService
    {
        Task<ForecastDto?> GetForecastAsync(CoordinatesDto coordinates);
    }
}
