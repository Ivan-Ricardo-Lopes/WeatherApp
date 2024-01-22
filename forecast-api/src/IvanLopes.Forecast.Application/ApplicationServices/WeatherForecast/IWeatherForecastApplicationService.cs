using FluentResults;
using IvanLopes.Forecast.Application.Dtos;

namespace IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast
{
    public interface IWeatherForecastApplicationService
    {
        Task<Result<ForecastDto>> GetWeatherForecastAsync(AddressInput address);
    }
}
