using weather_app.Utilities;
using weather_app.Weather;

string apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY")!;

if (string.IsNullOrEmpty(apiKey))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("API key not found. Please set the WEATHER_API_KEY environment variable.");
    Console.ResetColor();
    return;
}

Console.WriteLine("Welcome to Weather App!");
bool userWantsToContinue = true;

while (userWantsToContinue)
{
    Console.Clear();
    Console.Write("Please enter the city name you want weather data of: ");
    string cityNameInput = Console.ReadLine()!;

    WeatherData? weatherData = await WeatherService.GetWeatherData(cityNameInput, apiKey);

    if (weatherData != null)
    {
        WeatherService.PrintWeatherDataToConsole(weatherData);

        Console.Write("Do you want to save this data to a text file? (Y/N): ");
        string saveToFile = Console.ReadLine()!.ToUpper();

        if (saveToFile == "Y")
        {
            // output directory
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weather_data.txt");
            WeatherService.SaveWeatherDataToFile(weatherData, filePath);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Saved weather data of {weatherData.CityName} to the text file successfully!");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Unable to retrieve weather data");
        Console.ResetColor();
    }

    Console.WriteLine();
    Console.Write("Do you want to search for another city? (Y/N): ");
    string userResponse = Console.ReadLine()!.ToUpper();
    userWantsToContinue = (userResponse == "Y");
}

Console.WriteLine();
Console.WriteLine("Thank you for using Weather App!");
