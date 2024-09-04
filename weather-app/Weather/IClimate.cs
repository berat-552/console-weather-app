namespace weather_app.Weather;

public interface IClimate
{
    public double Temperature { get; set; }

    public double FeelsLike { get; set; }

    public int Pressure { get; set; }

    public int Humidity { get; set; }
}