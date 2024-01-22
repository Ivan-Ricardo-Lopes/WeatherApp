import { IconName } from "@fortawesome/free-solid-svg-icons";
import { DayPeriod, WeatherType } from "../Enums/Enums";

const IconMapper = (
  dayPeriod: DayPeriod,
  weatherType: WeatherType
): IconName => {
  switch (weatherType) {
    case WeatherType.ClearSunnySkies:
      return dayPeriod === DayPeriod.Day ? "sun" : "moon";
    case WeatherType.CloudySkies:
      return "cloud";
    case WeatherType.RainyConditions:
      return "cloud-rain";
    case WeatherType.WinterWeather:
      return "snowflake";
    case WeatherType.FoggyHazyConditions:
      return "smog";
    case WeatherType.WindyConditions:
      return "wind";
    case WeatherType.SevereWeather:
      return "bolt";
    case WeatherType.Unknown:
    default:
      return "question";
  }
};

export default IconMapper;
