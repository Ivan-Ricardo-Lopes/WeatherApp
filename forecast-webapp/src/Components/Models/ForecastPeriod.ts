import { DayPeriod, WeatherType } from "../../Enums/Enums";

class ForecastPeriod {
  constructor(
    name: string,
    dayPeriod: DayPeriod,
    weatherType: WeatherType,
    description: string,
    temperature: number,
    temperatureUnit: string,
    windDirection: string,
    windSpeed: string,
    relativeHumidityInPercentage: number
  ) {
    this.name = name;
    this.dayPeriod = dayPeriod;
    this.weatherType = weatherType;
    this.description = description;
    this.temperature = temperature;
    this.temperatureUnit = temperatureUnit;
    this.windDirection = windDirection;
    this.windSpeed = windSpeed;
    this.relativeHumidityInPercentage = relativeHumidityInPercentage;
  }
  name: string;
  dayPeriod: DayPeriod;
  weatherType: WeatherType;
  description: string;
  temperature: number;
  temperatureUnit: string;
  windDirection: string;
  windSpeed: string;
  relativeHumidityInPercentage: number;
}

export default ForecastPeriod;
