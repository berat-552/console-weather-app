# weather-app

## Overview
The Weather App is a console application that retrieves and displays weather data for a specified city. Users can choose to save the weather data to a file and view previously saved data.

## Features
- Retrieve weather data for any city using the OpenWeatherMap API.
- Display weather data in both Metric and Imperial units.
- Save weather data to a JSON file.
- View previously saved weather data.

## Prerequisites
- .NET SDK
- An API key from OpenWeatherMap. Set the `WEATHER_API_KEY` environment variable with your API key.
Usage
- Set up the API Key: Ensure you have set the `WEATHER_API_KEY` environment variable with your OpenWeatherMap API key.

## Setting Up the Weather API Key
### Windows
1. Open Command Prompt:
  - Press `Windows + R`, type `cmd`, and press Enter.

2. Set the API Key:
```sh
setx WEATHER_API_KEY "your_api_key_here"
```

4. Verify the API Key:
```sh
echo %WEATHER_API_KEY%
```
