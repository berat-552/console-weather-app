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

    Console.Write("The default units is 'Metric'. Would you like to change units to Imperial? (Y/N): ");

    string unitsInput = Console.ReadLine()!.ToUpper();

    bool isImperialUnits = (unitsInput.Equals("Y"));

    IWeatherData? weatherData = await WeatherService.GetWeatherData(cityNameInput, weatherApiKey, isImperialUnits);

    if (weatherData != null)
    {
        Console.WriteLine();
        Console.WriteLine(weatherData);
        Console.WriteLine();
        string saveToFile;

        do
        {
            Console.Write("Would you like to save the weather data to a file? (Y/N): ");
            saveToFile = Console.ReadLine()!.Trim().ToUpper();
        } while (saveToFile != "Y" && saveToFile != "N");

        if (saveToFile == "Y")
        {
            WeatherService.SaveDataAndNotify(weatherData, isImperialUnits);
        }
        else
        {
            Console.WriteLine("Weather data will not be saved to a file.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Weather data for city '{cityNameInput}' could not be retrieved. The city might not exist.");
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
    FileData? jsonMetricWeatherData = WeatherService.GetFileData("weather_data_metric.json");
    FileData? jsonImperialWeatherData = WeatherService.GetFileData("weather_data_imperial.json");

    WeatherService.DisplayWeatherData(jsonMetricWeatherData, "Metric");
    WeatherService.DisplayWeatherData(jsonImperialWeatherData, "Imperial");
}

string metricFilePath = WeatherService.GetJsonFileLocation("weather_data_metric.json");
string imperialFilePath = WeatherService.GetJsonFileLocation("weather_data_imperial.json");

if (File.Exists(metricFilePath) || File.Exists(imperialFilePath))
{
    Console.WriteLine();
    Console.Write("Would you like the erase all of the weather data? (Y/N): ");

    string deleteJsonFileResponse = Console.ReadLine()!.ToUpper();

    if (deleteJsonFileResponse == "Y")
    {
        WeatherService.EraseAllWeatherData(metricFilePath);
        WeatherService.EraseAllWeatherData(imperialFilePath);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Successfully deleted weather data");
        Console.WriteLine();
        Console.ResetColor();
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