using Newtonsoft.Json;

namespace TraxionWeather.Models
{
    /// <summary>
    /// Model class for weather json format API response
    /// </summary>
    public class WeatherInfo
    {
        public class weather_data
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        public class rain_data
        {
            [JsonProperty(PropertyName = "1h")]
            public double _1h { get; set; }
        }
        public class period_data
        {
            public int dt { get; set; }
            public double sunrise { get; set; }
            public double sunset { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double humidity { get; set; }
            public double dew_point { get; set; }
            public double uvi { get; set; }
            public double clouds { get; set; }
            public double visibility { get; set; }
            public double wind_speed { get; set; }
            public double wind_deg { get; set; }
            public weather_data[] weather { get; set; }
            public rain_data rain { get; set; }

        }
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public string timezone_offset { get; set; }
        public period_data current { get; set; }
        public period_data[] hourly { get; set; }
        public string date { get; set; }
        public string StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
    }
}
