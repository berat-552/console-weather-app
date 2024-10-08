using Newtonsoft.Json;

namespace weather_app.Weather;

public class Wind
{
    [JsonProperty("speed")]
    public double Speed { get; set; }

    [JsonProperty("deg")]
    public double Degree { get; set; }

    [JsonProperty("gust")]
    public double Gust { get; set; }
}