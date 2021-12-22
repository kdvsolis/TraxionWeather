using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraxionWeather.ViewModels;
using Xamarin.Forms;

namespace TraxionWeather
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            for(int i = 0; i < 5; i++)
            {
                DateTime dateVal = DateTime.UtcNow.AddDays(-1 * i);
                Button showWeatherDetails = new Button { Text = dateVal.ToString("dddd, dd MMMM yyyy") };
                showWeatherDetails.SetBinding(Button.CommandProperty, new Binding("ViewModelProperty"));
                showWeatherDetails.Margin = new Thickness(40, 10, 40, 10);
                parent.Children.Add(showWeatherDetails);
                showWeatherDetails.Clicked += async (sender, args) =>
                {
                    TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
                    TimeSpan unixTicks = new TimeSpan(dateVal.Ticks) - epochTicks;
                    double dtInSeconds = unixTicks.TotalSeconds;
                    int dt = (Int32)dtInSeconds;
                    WeatherViewModel weather = new WeatherViewModel();
                    await DisplayAlert("Weather details at " + dateVal.ToString("dddd, dd MMMM yyyy"), weather.GetWeatherDetails(dt.ToString()), "OK");
                };
            }
        }
    }
}
