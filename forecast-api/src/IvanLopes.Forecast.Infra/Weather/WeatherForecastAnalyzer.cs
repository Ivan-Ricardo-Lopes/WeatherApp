using IvanLopes.Forecast.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanLopes.Forecast.Infra.Weather
{

    public class WeatherForecastAnalyzer
    {
        static Dictionary<string, WeatherType> WeatherTypess = new Dictionary<string, WeatherType>
    {
        { "Sunny", WeatherType.ClearSunnySkies },
        { "Clear", WeatherType.ClearSunnySkies },
        { "Overcast", WeatherType.CloudySkies },
        { "Cloudy", WeatherType.CloudySkies },
        { "Rainy", WeatherType.RainyConditions },
        { "Rain", WeatherType.RainyConditions },
        { "Showers", WeatherType.RainyConditions },
        { "Drizzle", WeatherType.RainyConditions },
        { "Thunderstorms", WeatherType.RainyConditions },
        { "Scattered", WeatherType.RainyConditions },
        { "Heavy", WeatherType.RainyConditions },
        { "Light", WeatherType.RainyConditions },
        { "Hail", WeatherType.WinterWeather },
        { "Snow", WeatherType.WinterWeather },
        { "Sleet", WeatherType.WinterWeather },
        { "Freezing", WeatherType.WinterWeather },
        { "Flurries", WeatherType.WinterWeather },
        { "Blizzards", WeatherType.WinterWeather },
        { "Foggy", WeatherType.FoggyHazyConditions },
        { "Misty", WeatherType.FoggyHazyConditions },
        { "Hazy", WeatherType.FoggyHazyConditions },
        { "Dusty", WeatherType.FoggyHazyConditions },
        { "Smoky", WeatherType.FoggyHazyConditions },
        { "Windy", WeatherType.WindyConditions },
        { "Breezy", WeatherType.WindyConditions },
        { "Tornadoes", WeatherType.SevereWeather },
        { "Hurricane", WeatherType.SevereWeather },
        { "Typhoon", WeatherType.SevereWeather },
        { "Cyclone", WeatherType.SevereWeather },
        { "Tropics", WeatherType.SevereWeather }
    };

        public static WeatherType DetermineWeatherTypes(string phrase)
        {
            string[] words = phrase.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (WeatherTypess.ContainsKey(word))
                {
                    return WeatherTypess[word];
                }
            }

            return WeatherType.Unknown;
        }
    }

}
