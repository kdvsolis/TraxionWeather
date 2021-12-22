using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TraxionWeather.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public class Currency
        {
            public string code { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
        }

        public class TimeZone
        {
            public string name { get; set; }
            public int offset { get; set; }
            public string current_time { get; set; }
            public double current_time_unix { get; set; }
            public bool is_dst { get; set; }
            public int dst_savings { get; set; }
        }
        public class Location
        {
            public string ip { get; set; }
            public string continent_code { get; set; }
            public string continent_name { get; set; }
            public string country_code2 { get; set; }
            public string country_code3 { get; set; }
            public string country_name { get; set; }
            public string country_capital { get; set; }
            public string state_prov { get; set; }
            public string district { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public bool is_eu { get; set; }
            public string calling_code { get; set; }
            public string country_tld { get; set; }
            public string languages { get; set; }
            public string country_flag { get; set; }
            public string geoname_id { get; set; }
            public string isp { get; set; }
            public string connection_type { get; set; }
            public string organization { get; set; }
            public Currency currency { get; set; }
            public TimeZone time_zone { get; set; }
        }
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
        public class WeatherInfo
        {
            public double lat { get; set; }
            public double lon { get; set; }
            public string timezone { get; set; }
            public string timezone_offset { get; set; }
            public period_data current { get; set; }
            public period_data[] hourly { get; set; }
        }
        private WeatherInfo weatherInfo;
        public WeatherInfo overallWeatherInfo
        {
            get { return weatherInfo; }
            set
            {
                weatherInfo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("WeatherInfo"));
            }
        }

        public WeatherViewModel()
        {
        }
        public string GetWeatherDetails(string date)
        {
            //Email used to register: madove7001@videour.com
            //Intentionally hardcoded the details to save time

            HttpClient clientLocation = new HttpClient();
            clientLocation.BaseAddress = new Uri("https://api.ipgeolocation.io");
            clientLocation.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = clientLocation.GetAsync("ipgeo?apiKey=53fbbb0f90604e00904946f34d3a0e32").Result;
            if (response.IsSuccessStatusCode)
            {
                Location resultLocInfo = JsonConvert.DeserializeObject<Location>(response.Content.ReadAsStringAsync().Result);
                HttpClient clientWeather = new HttpClient();
                clientWeather.BaseAddress = new Uri("https://api.openweathermap.org");
                clientWeather.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = clientWeather.GetAsync("data/2.5/onecall/timemachine?lat=" + resultLocInfo.latitude + "&lon=" + resultLocInfo.longitude + "&exclude=hourly&dt=" + date + "&appid=a642e4aac2a034bbc63c01170e554564").Result;
                if (response.IsSuccessStatusCode)
                {
                    WeatherInfo resultWeatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response.Content.ReadAsStringAsync().Result);
                    return "Location: " + resultLocInfo.city + "/" + resultLocInfo.country_name + "\n" +
                           "Weather: " + resultWeatherInfo.current.weather[0].main + "\n" +
                           "Description: " + resultWeatherInfo.current.weather[0].description + "\n";
                }
                else
                {
                    return "Error\nStatus Code: " + response.StatusCode.ToString() + "\nReason: " + response.ReasonPhrase;
                }
            }
            else
            {
                return "Error\nStatus Code: " + response.StatusCode.ToString() + "\nReason: " + response.ReasonPhrase;
            }
        }
    }
}
