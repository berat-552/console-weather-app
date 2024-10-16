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

        InitializeWeatherDataFile(filePath);

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            weatherDataList = JsonConvert.DeserializeObject<List<object>>(existingJson) ?? [];
        }
        else
        {
            weatherDataList = [];
        }

        weatherDataList.Add(weatherData);

        string jsonString = JsonConvert.SerializeObject(weatherDataList, Formatting.Indented);

        File.WriteAllText(filePath, jsonString);
    }

    public static void SaveDataAndNotify(IWeatherData weatherData, bool isImperialUnits)
    {
        string fileName = isImperialUnits ? "weather_data_imperial.json" : "weather_data_metric.json";
        string filePath = GetJsonFileLocation(fileName);

        SaveWeatherDataToFile(weatherData, filePath);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Saved weather data of {weatherData.CityName} to the text file successfully!");
    }

    public static FileData? GetFileData(string filepath)
    {
        if (!File.Exists(filepath))
        {
            return null;
        }

        return new FileData
        {
            Data = File.ReadAllText(filepath),
            FilePath = filepath
        };
    }

    public static void DisplayWeatherData(FileData? weatherData, string unit)
    {
        if (weatherData?.Data != null)
        {
            Console.WriteLine("=============================");
            Console.WriteLine($"JSON Weather Data ({unit}):");
            Console.WriteLine();
            Console.WriteLine(weatherData.Data);
            Console.WriteLine();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{unit} weather data is empty.");
            Console.WriteLine();
            Console.ResetColor();
        }
    }

    public static string GetJsonFileLocation(string filename) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

        public static void EraseAllWeatherData(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return;
            }

            File.Delete(filepath);
        }
    }
