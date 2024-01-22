namespace IvanLopes.Forecast.Application.ApplicationServices.WeatherForecast
{
    public enum WeatherGroup
    {
        ClearSunnySkies,
        CloudySkies,
        RainyConditions,
        WinterWeather,
        FoggyHazyConditions,
        WindyConditions,
        SevereWeather,
        Unknown
    }

    public class WeatherForecastAnalyzer
    {
        static Dictionary<string, WeatherGroup> weatherGroups = new Dictionary<string, WeatherGroup>
    {
        { "Sunny", WeatherGroup.ClearSunnySkies },
        { "Clear", WeatherGroup.ClearSunnySkies },
        { "Partly", WeatherGroup.CloudySkies },
        { "Mostly", WeatherGroup.CloudySkies },
        { "Overcast", WeatherGroup.CloudySkies },
        { "Cloudy", WeatherGroup.CloudySkies },
        { "Rainy", WeatherGroup.RainyConditions },
        { "Showers", WeatherGroup.RainyConditions },
        { "Drizzle", WeatherGroup.RainyConditions },
        { "Thunderstorms", WeatherGroup.RainyConditions },
        { "Scattered", WeatherGroup.RainyConditions },
        { "Heavy", WeatherGroup.RainyConditions },
        { "Light", WeatherGroup.RainyConditions },
        { "Chance", WeatherGroup.RainyConditions },
        { "Hail", WeatherGroup.WinterWeather },
        { "Snow", WeatherGroup.WinterWeather },
        { "Sleet", WeatherGroup.WinterWeather },
        { "Freezing", WeatherGroup.WinterWeather },
        { "Flurries", WeatherGroup.WinterWeather },
        { "Blizzards", WeatherGroup.WinterWeather },
        { "Foggy", WeatherGroup.FoggyHazyConditions },
        { "Misty", WeatherGroup.FoggyHazyConditions },
        { "Hazy", WeatherGroup.FoggyHazyConditions },
        { "Dusty", WeatherGroup.FoggyHazyConditions },
        { "Smoky", WeatherGroup.FoggyHazyConditions },
        { "Windy", WeatherGroup.WindyConditions },
        { "Breezy", WeatherGroup.WindyConditions },
        { "Tornadoes", WeatherGroup.SevereWeather },
        { "Hurricane", WeatherGroup.SevereWeather },
        { "Typhoon", WeatherGroup.SevereWeather },
        { "Cyclone", WeatherGroup.SevereWeather },
        { "Tropics", WeatherGroup.SevereWeather }
    };

        public static WeatherGroup DetermineWeatherGroup(string phrase)
        {
            string[] words = phrase.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (weatherGroups.ContainsKey(word))
                {
                    return weatherGroups[word];
                }
            }

            return WeatherGroup.Unknown;
        }
    }

}
