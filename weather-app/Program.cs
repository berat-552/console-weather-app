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
    Console.ResetColor();
    Console.Write("Enter city name for weather data: ");
    string cityNameInput = Console.ReadLine()!;

    Console.WriteLine("Default units is 'Metric'");
    Console.Write("Change units to Imperial? (Y/N): ");

    string unitsInput = Console.ReadLine()!.ToUpper();

    bool isImperialUnits = (unitsInput.Equals("Y"));

    WeatherData? weatherData = await WeatherService.GetWeatherData(cityNameInput, apiKey, isImperialUnits);

    if (weatherData != null)
    {
        WeatherService.PrintWeatherDataToConsole(weatherData, isImperialUnits);

        string saveToFile = "";

        do
        {
            Console.Write("Would you like to save the weather data to a file? (Y/N): ");
            saveToFile = Console.ReadLine()!.Trim().ToUpper();
        } while (saveToFile != "Y" && saveToFile != "N");

        if (saveToFile == "Y")
        {
            // output directory
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weather_data.json");
            WeatherService.SaveWeatherDataToFile(weatherData, filePath);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Saved weather data of {weatherData.CityName} to the text file successfully!");
        }
        else
        {
            Console.WriteLine("Weather data will not be saved to a file.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Unable to retrieve weather data");
        Console.ResetColor();
    }

    Console.WriteLine();
    Console.ResetColor();
    Console.Write("Do you want to search for another city? (Y/N): ");
    string userResponse = Console.ReadLine()!.ToUpper();
    userWantsToContinue = (userResponse == "Y");
}

Console.WriteLine();

Console.Write("Would you like to view the already existing JSON weather data? (Y/N): ");

string viewJsonResponse = Console.ReadLine()!.ToUpper();

if (viewJsonResponse == "Y")
{
    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weather_data.json");
    string jsonData = WeatherService.ReadJsonFromFile(filePath);

    if (!string.IsNullOrEmpty(jsonData))
    {
        Console.WriteLine("JSON Weather Data:");
        Console.WriteLine();
        Console.WriteLine(jsonData);
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("No weather data found in the file.");
    }
}

Console.WriteLine("Thank you for using Weather App!");
Console.WriteLine("Goodbye!");