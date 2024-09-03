using Moq;
using Newtonsoft.Json;
using weather_app.Utilities;
using weather_app.Weather;
using Xunit;

public interface IWeatherService
{
    Task<WeatherData?> GetWeatherData(string cityName, string apiKey, bool isImperialUnits);
    void EraseAllWeatherData(string filepath);
}

public class WeatherServiceTests
{
    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weather_data.json");
    private readonly Mock<IWeatherService> mockWeatherService = new Mock<IWeatherService>();

    [Fact]
    public async Task GetWeatherData_MethodIsCalledAndNotNull()
    {
        string cityName = "London";
        string apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY")!; 
        bool isImperialUnits = false;

        await mockWeatherService.Object.GetWeatherData(cityName, apiKey, isImperialUnits);

        WeatherData? result = await WeatherService.GetWeatherData(cityName, apiKey, isImperialUnits);

        mockWeatherService.Verify(service => service.GetWeatherData(cityName, apiKey, isImperialUnits), Times.Once);

        Assert.NotNull(result);
        Assert.Equal(cityName, result.CityName);
    }

    [Fact]
    public void PrintWeatherDataToConsole_PrintsOutput()
    {
        WeatherData weatherData = new WeatherData
        {
            CityName = "London",
            Main = new Main { Temp = 15.0, FeelsLike = 14.0, Pressure = 1012, Humidity = 80 },
            Weather = new List<Weather> { new Weather { Description = "clear sky" } },
            Wind = new Wind { Speed = 5.0 }
        };
        bool isImperialUnits = false;

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Act
            WeatherService.PrintWeatherDataToConsole(weatherData, isImperialUnits);

            // Assert
            string result = sw.ToString();
            Assert.Contains("City: London", result);
            Assert.Contains("Temperature: 15 �C", result);
        }
    }

    [Fact]
    public void InitializeWeatherDataFile_InitializesFileSuccessfully()
    {
        // output directory
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weather_data.json");

        WeatherService.InitializeWeatherDataFile(filePath);

        Assert.True(File.Exists(filePath), "The weather data file was not created.");
    }

    [Fact]
    public void SaveWeatherDataToFile_SavesDataCorrectly()
    {
        WeatherData weatherData = new WeatherData
        {
            CityName = "London",
            Main = new Main { Temp = 15.0, FeelsLike = 14.0, Pressure = 1012, Humidity = 80 },
            Weather = new List<Weather> { new Weather { Description = "clear sky" } },
            Wind = new Wind { Speed = 5.0 }
        };

        WeatherService.SaveWeatherDataToFile(weatherData, filePath);

        Assert.True(File.Exists(filePath), "The weather data file was not created.");
        string fileContents = File.ReadAllText(filePath);
        List<WeatherData> weatherDataList = JsonConvert.DeserializeObject<List<WeatherData>>(fileContents)!;

        Assert.NotNull(weatherDataList);
        Assert.Contains(weatherDataList, wd => wd.CityName == "London");

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public void ReadJsonFile_ReadsFileAndReturnsJsonString()
    {
        string jsonContent = WeatherService.ReadJsonFromFile(filePath);

        Assert.NotNull(jsonContent);
        Assert.IsType<string>(jsonContent);
    }

    [Fact]
    public void GetJsonFileLocation_GetsTheFileLocation()
    {
        string fileLocation = WeatherService.GetJsonFileLocation();
        Assert.NotNull(fileLocation);
        Assert.IsType<string>(fileLocation);
    }

    [Fact]
    public void EraseAllWeatherData_ErasesWeatherDataFile()
    {
        mockWeatherService.Object.EraseAllWeatherData(filePath);
        mockWeatherService.Verify(verify => verify.EraseAllWeatherData(filePath), Times.Once());
        // check that file no longer exists
        Assert.True(!File.Exists(filePath));
    }

}
