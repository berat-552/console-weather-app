using Newtonsoft.Json;

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

    public override string ToString() =>
        $"Temperature: {Temperature} {_units.Temperature}\n" +
        $"Feels Like: {FeelsLike} {_units.Temperature}\n" +
        $"Pressure: {Pressure} hPa\n" +
        $"Humidity: {Humidity}%\n";
}
