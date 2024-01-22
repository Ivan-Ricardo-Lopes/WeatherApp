import React from "react";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faWind,
  faTemperatureLow,
  faTemperatureHigh,
  faSun,
  faSnowflake,
  faMoon,
  faCloudRain,
  faCloud,
  faBolt,
  faWater,
  faCloudSunRain,
} from "@fortawesome/free-solid-svg-icons";
import ForecastPage from "./Components/ForecastPage/ForecastPage";
library.add(
  faWind,
  faTemperatureLow,
  faTemperatureHigh,
  faSun,
  faSnowflake,
  faMoon,
  faCloudRain,
  faCloud,
  faBolt,
  faWater,
  faCloudSunRain
);

const App: React.FC = () => {
  return (
    <div className="app flex row-container center">
      <ForecastPage />
    </div>
  );
};

export default App;
