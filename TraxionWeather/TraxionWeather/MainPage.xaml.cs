using System;
using TraxionWeather.ViewModels;
using Xamarin.Forms;

namespace TraxionWeather
{
    /// <summary>
    /// Class Entry point
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Entry point
        /// </summary>
        public MainPage()
        {
            WeatherViewModel weather = new WeatherViewModel();
            this.BindingContext = weather;
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                DateTime dateVal = DateTime.UtcNow.AddDays(-1 * i);
                Button showWeatherDetails = new Button { Text = dateVal.ToString("dddd, dd MMMM yyyy") };
                showWeatherDetails.SetBinding(Button.CommandProperty, new Binding("ViewModelProperty"));
                showWeatherDetails.Margin = new Thickness(40, 10, 40, 10);
                parent.Children.Add(showWeatherDetails);
                showWeatherDetails.Clicked += async (sender, args) =>
                {
                    dateWord.Text = "Date: " + dateVal.ToString("dddd, dd MMMM yyyy");
                    int dt = (Int32)(new TimeSpan(dateVal.Ticks) - new TimeSpan(new DateTime(1970, 1, 1).Ticks)).TotalSeconds;
                    weather.GetWeatherDetails(dt.ToString());
                    if(weather.overallWeatherInfo.StatusCode != null)
                    {
                        await DisplayAlert("Error", weather.overallWeatherInfo.ReasonPhrase, "OK");
                    }
                };
            }
        }
    }
}
