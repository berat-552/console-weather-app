﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather_app.Weather;

namespace weather_app.Utilities
{
    internal class WeatherService
    {
        public static async Task<WeatherData?> GetWeatherData(string cityNameInput, string apiKey)
        {
            string baseUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityNameInput}&appid={apiKey}&units=metric";

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

        public static void PrintWeatherDataToConsole(WeatherData weatherData)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"SUCCESSFUL");
            Console.ResetColor();
            Console.WriteLine("===========================================");

            Console.WriteLine($"City: {weatherData.CityName}");
            Console.WriteLine($"Temperature: {weatherData.Main.Temp}°C");
            Console.WriteLine($"Feels Like: {weatherData.Main.FeelsLike}°C");
            Console.WriteLine($"Description: {weatherData.Weather[0].Description}");
            Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} m/s");
            Console.WriteLine($"Pressure: {weatherData.Main.Pressure} hPa");
            Console.WriteLine($"Humidity: {weatherData.Main.Humidity}%");
            Console.WriteLine();
        }

        public static void SaveWeatherDataToFile(WeatherData weatherData, string filePath)
        {
            string weatherContent = $"{weatherData.CityName},{weatherData.Main.Temp},{weatherData.Main.FeelsLike},{weatherData.Weather[0].Description},{weatherData.Wind.Speed},{weatherData.Main.Pressure},{weatherData.Main.Humidity}\n";

            File.AppendAllText(filePath, weatherContent);
        }
    }
}