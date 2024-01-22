import { DayOfWeek } from "../../Enums/Enums";
import ForecastPeriod from "./ForecastPeriod";

class ForecastDay {
  constructor(
    isToday: boolean,
    dayOfWeek: DayOfWeek,
    forecastPeriods: ForecastPeriod[]
  ) {
    this.isToday = isToday;
    this.dayOfWeek = dayOfWeek;
    this.forecastPeriods = forecastPeriods;
  }
  isToday: boolean;
  dayOfWeek: DayOfWeek;
  forecastPeriods: ForecastPeriod[];
}

export default ForecastDay;
