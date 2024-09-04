using Newtonsoft.Json;
using System.Text.Json;

namespace weather_app.Weather;

public class WeatherData<TUnits> : IWeatherData
    where TUnits : IMeasurementUnits, new()
{
    [JsonProperty("name")]
    public string CityName { get; set; } = string.Empty;

    [JsonProperty("main")]
    public IClimate Climate { get; set; } = new Climate<TUnits>();

    [JsonProperty("weather")]
    public List<Weather> Weather { get; set; } = new List<Weather>();

    [JsonProperty("wind")]
    public Wind Wind { get; set; } = new Wind();

    public IMeasurementUnits Units { get; } = new TUnits();

    public override string ToString()
    {
;
        //Console.WriteLine($"Description: {weatherData.Weather[0].Description}");
        //Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} {windSpeedUnit}");

        return $"City: {CityName}\n" +
            $"{Climate}\n" +
            $"Wind Speed: {Wind.Speed}\n";
    }
}
