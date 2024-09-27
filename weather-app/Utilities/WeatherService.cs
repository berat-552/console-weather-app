using Newtonsoft.Json;
using weather_app.Weather;
using weather_app.Weather.Units;

namespace weather_app.Utilities;

public class WeatherService
{
    public static async Task<IWeatherData?> GetWeatherData(string cityNameInput, string weatherApiKey, bool isImperialUnits)
    {
        string units = isImperialUnits ? "imperial" : "metric";
        string baseUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityNameInput}&appid={weatherApiKey}&units={units}";

        HttpClient client = new HttpClient();

        HttpResponseMessage response = client.GetAsync(baseUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            string responseAsString = await response.Content.ReadAsStringAsync();
            return isImperialUnits
                ? JsonConvert.DeserializeObject<WeatherData<Imperial>>(responseAsString)
                : JsonConvert.DeserializeObject<WeatherData<Metric>>(responseAsString);
        }

        return null;
    }

    public static void InitializeWeatherDataFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public static void SaveWeatherDataToFile(IWeatherData weatherData, string filePath)
    {
        List<object> weatherDataList;

        InitializeWeatherDataFile(filePath); // Ensure the file is initialized

        // Check if the file exists and read the existing data
        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            weatherDataList = JsonConvert.DeserializeObject<List<object>>(existingJson) ?? [];
        }
        else
        {
            weatherDataList = [];
        }

        // Add the new weather data to the list
        weatherDataList.Add(weatherData);

        // Serialize the list to a JSON string
        string jsonString = JsonConvert.SerializeObject(weatherDataList, Formatting.Indented);

        File.WriteAllText(filePath, jsonString);
    }

    public static string ReadJsonFromFile(string filePath) => File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;

    public static string GetJsonFileLocation(string filename) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

    public static void EraseAllWeatherData(string filepath)
    {
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
    }
}
