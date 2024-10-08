using Newtonsoft.Json;
using System.Text.Json;
using weather_app.Weather.Units;

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

    [JsonProperty("sys")]
    public SystemInfo SystemInfo = new SystemInfo();

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    public IMeasurementUnits Units { get; } = new TUnits();

    public override string ToString()
    {
        return $"{SystemInfo}" +
            $"Timezone: {Timezone} UTC \n" +
            $"City: {CityName}\n" +
            $"Description: {Weather[0].Description}\n" +
            $"{Climate}\n" +
            $"Wind Speed: {Wind.Speed} {Units.Speed}\n" +
            $"Wind Gust: {Wind.Gust} {Units.Temperature}\n" +
            $"Wind Degree: {Wind.Degree} {Units.Temperature}\n" +
            $"Visibility: {Visibility}";
    }
}
