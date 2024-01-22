enum WeatherType {
  ClearSunnySkies,
  CloudySkies,
  RainyConditions,
  WinterWeather,
  FoggyHazyConditions,
  WindyConditions,
  SevereWeather,
  Unknown,
}

enum DayPeriod {
  Day,
  Night,
}

enum DayOfWeek {
  Sunday = 0,
  Monday = 1,
  Tuesday = 2,
  Wednesday = 3,
  Thursday = 4,
  Friday = 5,
  Saturday = 6,
}

export { WeatherType, DayPeriod, DayOfWeek };
