using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using TraxionWeather.Models;

namespace TraxionWeather.ViewModels
{
    /// <summary>
    /// ViewModel Class for Weather API
    /// </summary>
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private WeatherInfo weatherInfo;
        public WeatherInfo overallWeatherInfo
        {
            get { return weatherInfo; }
            set
            {
                weatherInfo = value;
                NotifyPropertyChanged();
            }
        }
        private Location location;
        public Location overallLocationInfo
        {
            get { return location; }
            set
            {
                location = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public WeatherViewModel()
        {
        }
        /// <summary>
        /// Function for notifying property changes
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Calling APIs to get weather details
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WeatherInfo GetWeatherDetails(string date)
        {
            //Email used to register: madove7001@videour.com
            //Intentionally hardcoded the details to save time

            HttpClient clientLocation = new HttpClient();
            clientLocation.BaseAddress = new Uri("https://api.ipgeolocation.io");
            clientLocation.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = clientLocation.GetAsync("ipgeo?apiKey=53fbbb0f90604e00904946f34d3a0e32").Result;
            if (response.IsSuccessStatusCode)
            {
                overallLocationInfo = JsonConvert.DeserializeObject<Location>(response.Content.ReadAsStringAsync().Result);
                HttpClient clientWeather = new HttpClient();
                clientWeather.BaseAddress = new Uri("https://api.openweathermap.org");
                clientWeather.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = clientWeather.GetAsync("data/2.5/onecall/timemachine?lat=" + location.latitude + "&lon=" + location.longitude + "&exclude=hourly&dt=" + date + "&appid=a642e4aac2a034bbc63c01170e554564").Result;
                if (response.IsSuccessStatusCode)
                {
                    overallWeatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response.Content.ReadAsStringAsync().Result);
                    return overallWeatherInfo;
                }
                else
                {
                    overallWeatherInfo = JsonConvert.DeserializeObject<WeatherInfo>("{'StatusCode': '" + response.StatusCode.ToString() + "' ,'ReasonPhrase': '" + response.ReasonPhrase + "'}");
                    return overallWeatherInfo;
                }
            }
            else
            {
                overallWeatherInfo = JsonConvert.DeserializeObject<WeatherInfo>("{'StatusCode': '" + response.StatusCode.ToString() + "' ,'ReasonPhrase': '" + response.ReasonPhrase + "'}");
                return overallWeatherInfo;
            }
        }
    }
}
