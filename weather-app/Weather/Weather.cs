using Newtonsoft.Json;

namespace weather_app.Weather;

public class Weather
{
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
}

