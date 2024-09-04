using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_app.Weather;

public class Weather
{
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
}

