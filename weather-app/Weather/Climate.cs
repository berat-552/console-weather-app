using Newtonsoft.Json;
using weather_app.Weather.Units;

namespace weather_app.Weather;

public class Climate<TUnits> : IClimate
    where TUnits : IMeasurementUnits, new()
{
    private readonly TUnits _units = new();

    [JsonProperty("temp")]
    public double Temperature { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("temp_min")]
    public double MinimumTemperature { get; set; }

    [JsonProperty("temp_max")]
    public double MaximumTemperature { get; set; }

    [JsonProperty("sea_level")]
    public int SeaLevel { get; set; }

    [JsonProperty("grnd_level")]
    public int GroundLevel { get; set; }

    [JsonProperty("visibility")]
    public int Visibility { get; set; }

    public override string ToString()
    {
        return $"Temperature: {Temperature} {_units.Temperature}\n" +
        $"Minimum Temperature: {MinimumTemperature}\n" +
        $"Maximum Temperature: {MaximumTemperature}\n" +
        $"Feels Like: {FeelsLike} {_units.Temperature}\n" +
        $"Pressure: {Pressure} hPa\n" +
        $"Humidity: {Humidity}%\n" +
        $"Sea Level: {SeaLevel} hPa\n" +
        $"Ground Level: {GroundLevel} hPa";
    }
}
