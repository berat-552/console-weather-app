namespace weather_app.Weather;

public interface IClimate
{
    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double MinimumTemperature { get; set; }
    public double MaximumTemperature { get; set; }
    public int SeaLevel { get; set; }
    public int GroundLevel { get; set; }
    public int Visibility { get; set; }
}