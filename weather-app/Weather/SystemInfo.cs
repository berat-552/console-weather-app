using Newtonsoft.Json;

namespace weather_app.Weather;

public class SystemInfo
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; } = string.Empty;

    [JsonProperty("sunrise")]
    public int Sunrise { get; set; }

    [JsonProperty("sunset")]
    public int Sunset { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}\n" +
                $"Country: {Country}\n" +
                $"Sunrise: {Sunrise}\n" +
                $"Sunset: {Sunset}\n";
    }
}
