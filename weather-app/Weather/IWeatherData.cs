using weather_app.Weather.Units;

namespace weather_app.Weather;

public interface IWeatherData
{
    public string CityName { get; set; }
    public IClimate Climate { get; set; }
    public List<Weather> Weather { get; set; }
    public Wind Wind { get; set; }
    public IMeasurementUnits Units { get; }
}
