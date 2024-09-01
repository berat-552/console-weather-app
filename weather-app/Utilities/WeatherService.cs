using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using weather_app.Weather;

namespace weather_app.Utilities
{
    internal class WeatherService
    {
        public static async Task<WeatherData?> GetWeatherData(string cityNameInput, string apiKey, bool isImperialUnits)
        {
            string units = isImperialUnits ? "imperial " : "metric";
            string baseUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityNameInput}&appid={apiKey}&units={units}";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseAsString = await response.Content.ReadAsStringAsync();
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseAsString)!;

                return weatherData;
            }

            return null;
        }

        public static void PrintWeatherDataToConsole(WeatherData weatherData, bool isImperialUnits)
        {
            string temperatureSymbol = isImperialUnits ? "°F" : "°C";
            string windSpeedUnit = isImperialUnits ? "mph" : "m/s";

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"SUCCESSFUL");
            Console.ResetColor();
            Console.WriteLine("===========================================");

            Console.WriteLine($"City: {weatherData.CityName}");
            Console.WriteLine($"Temperature: {weatherData.Main.Temp} {temperatureSymbol}");
            Console.WriteLine($"Feels Like: {weatherData.Main.FeelsLike} {temperatureSymbol}");
            Console.WriteLine($"Description: {weatherData.Weather[0].Description}");
            Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} {windSpeedUnit}");
            Console.WriteLine($"Pressure: {weatherData.Main.Pressure} hPa");
            Console.WriteLine($"Humidity: {weatherData.Main.Humidity}%");
            Console.WriteLine();
        }

        public static void InitializeFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public static void SaveWeatherDataToFile(WeatherData weatherData, string filePath)
        {
            List<WeatherData> weatherDataList;

            InitializeFile(filePath); // Ensure the file is initialized

            // Check if the file exists and read the existing data
            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                weatherDataList = JsonConvert.DeserializeObject<List<WeatherData>>(existingJson) ?? new List<WeatherData>();
            }
            else
            {
                weatherDataList = new List<WeatherData>();
            }

            // Add the new weather data to the list
            weatherDataList.Add(weatherData);

            // Serialize the list to a JSON string
            string jsonString = JsonConvert.SerializeObject(weatherDataList, Formatting.Indented);

            File.WriteAllText(filePath, jsonString);
        }

        public static string ReadJsonFromFile(string filePath) => File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
    }
}
