namespace weather_app.Weather.Units;

public sealed class Imperial : IMeasurementUnits
{
    public string Temperature => "°F";

    public string Speed => "mph";
}