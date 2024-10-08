using weather_app.Utilities;
using weather_app.Weather;

string weatherApiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY")!;

if (string.IsNullOrEmpty(weatherApiKey))
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

    IWeatherData? weatherData = await WeatherService.GetWeatherData(cityNameInput, weatherApiKey, isImperialUnits);
    // refactor to throw error earlier
    if (weatherData != null)
    {
        Console.WriteLine();
        Console.WriteLine(weatherData);
        string saveToFile;

        do
        {
            Console.Write("Would you like to save the weather data to a file? (Y/N): ");
            saveToFile = Console.ReadLine()!.Trim().ToUpper();
        } while (saveToFile != "Y" && saveToFile != "N");

        if (saveToFile == "Y")
        {
            if (isImperialUnits)
            {
                string filePath = WeatherService.GetJsonFileLocation("weather_data_imperial.json");
                WeatherService.InitializeWeatherDataFile(filePath);
                WeatherService.SaveWeatherDataToFile(weatherData, filePath);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Saved weather data of {weatherData.CityName} to the text file successfully!");
            }
            else
            {
                // output directory
                string filePath = WeatherService.GetJsonFileLocation("weather_data_metric.json");
                WeatherService.SaveWeatherDataToFile(weatherData, filePath);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Saved weather data of {weatherData.CityName} to the text file successfully!");
            }
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
    string jsonMetricWeatherData = WeatherService.ReadJsonFromFile("weather_data_metric.json");
    string jsonImperialWeatherData = WeatherService.ReadJsonFromFile("weather_data_imperial.json");

    if (!string.IsNullOrEmpty(jsonMetricWeatherData))
    {
        Console.WriteLine("=============================");
        Console.WriteLine("JSON Weather Data (Metric):");
        Console.WriteLine();
        Console.WriteLine(jsonMetricWeatherData);
        Console.WriteLine();
        Console.WriteLine("=============================");
        Console.WriteLine("JSON Weather Data (Imperial):");
        Console.WriteLine();
        Console.WriteLine(jsonImperialWeatherData);
    }
    else
    {
        Console.WriteLine("No weather data found in the file.");
        Console.WriteLine();
    }
}

if (File.Exists(WeatherService.GetJsonFileLocation("weather_data_metric.json")) || File.Exists(WeatherService.GetJsonFileLocation("weather_data_imperial.json")))
{
    Console.Write("Would you like the erase all of the weather data? (Y/N): ");

    string deleteJsonFileResponse = Console.ReadLine()!.ToUpper();

    if (deleteJsonFileResponse == "Y")
    {
        WeatherService.EraseAllWeatherData(WeatherService.GetJsonFileLocation("weather_data_metric.json"));
        WeatherService.EraseAllWeatherData(WeatherService.GetJsonFileLocation("weather_data_imperial.json"));
        Console.WriteLine();
        Console.WriteLine("Successfully deleted weather data");
        Console.WriteLine();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("User chose the option to NOT delete weather data");
        Console.WriteLine();
        Console.ResetColor();
    }
}

Console.WriteLine("Thank you for using Weather App!");
Console.WriteLine("Goodbye!");