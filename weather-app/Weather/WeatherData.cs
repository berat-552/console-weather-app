using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace weather_app.Weather
{
    internal class WeatherData
    {

        //public WeatherData(string name, Main main, List<Weather> weather, Wind wind)
        //{
        //    this.CityName = name;
        //    this.Main.Temp = main.Temp;
        //    this.Main.Pressure = main.Pressure;
        //    this.Main.FeelsLike = main.FeelsLike;
        //    this.Main.Humidity = main.Humidity;
        //    this.Weather = weather;
        //    this.Wind = wind;
        //}

        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }
}
