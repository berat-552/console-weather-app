using Newtonsoft.Json;
using weather_app.Weather.Units;

namespace weather_app.Weather;

public class Wind
{
    [JsonProperty("speed")]
    public double Speed { get; set; }
}

public interface IWind
{
    public double Speed { get; set; }
}