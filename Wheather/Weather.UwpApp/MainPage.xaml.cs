using System;
using System.Collections.Generic;
using System.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weather.UwpApp
{
    using System.Collections.ObjectModel;
    using System.Net.Http;

    using Newtonsoft.Json;

    using Weather.UwpApp.Models;

    using Wheather.Models.Now;
    using Wheather.Models.Seven;
    using Wheather.Models.Three;

    using Weather = Weather.UwpApp.Models.Weather;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.GetWeather.Click += GetWeather_Click;

            this.GetHistory.Click += GetHistory_Click;

            this.InitCities();
        }

        private async void GetHistory_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var value = await client.GetStringAsync("http://localhost:31657/Home/GetHistory");
                    var resp = JsonConvert.DeserializeObject<IEnumerable<HistoryItem>>(value);
                    this.History.Clear();
                    resp.ToList().ForEach(this.History.Add);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                
            }
        }

        private async void InitCities()
        {
            this.CityCB.Items.Clear();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var value = await httpClient.GetAsync("http://localhost:31657/Home/GetCities");
                    var cities = JsonConvert.DeserializeObject<IEnumerable<string>>(await value.Content.ReadAsStringAsync());
                    foreach (var city in cities)
                    {
                        this.CityCB.Items.Add(city);
                    }
                    this.CityCB.SelectedItem = "Lviv";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public ObservableCollection<HistoryItem> History = new ObservableCollection<HistoryItem>();

        public ObservableCollection<Weather> Weather { get; private set; } = new ObservableCollection<Weather>();

        private async void GetWeather_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var city = string.IsNullOrEmpty(this.CitiTB.Text) ? (string)this.CityCB.SelectedItem : this.CitiTB.Text;
                switch (this.Now.IsChecked ?? false ? 1 : this.Three.IsChecked ?? false ? 3 : 7)
                {
                    case 1:
                        var now = JsonConvert.DeserializeObject<Now>(
                            await client.GetStringAsync("http://localhost:31657/Home/GetWeatherNowJson?city=" + city));
                        this.Weather.Clear();
                        this.Weather.Add(new Weather
                                             {
                                                 City = now.Name,
                                                 Date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(now.Dt).Date.ToString("D"),
                                                 Icon = $"http://openweathermap.org/img/w/{now.Weather[0].Icon}.png",
                                                 Max = now.Main.Temp,
                                                 Min = now.Main.Temp
                                             });
                        break;
                    case 3:
                        var three = JsonConvert.DeserializeObject<Three>(
                            await client.GetStringAsync("http://localhost:31657/Home/GetWeatherThreeDaysJson?city=" + city));
                        this.Weather.Clear();
                        three.List.Select(w => new Weather
                                                   {
                                                       Max = w.Temp.Max,
                                                       Min = w.Temp.Min,
                                                       Date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt).Date.ToString("D"),
                                                       Icon = $"http://openweathermap.org/img/w/{w.Weather[0].Icon}.png",
                                                       City = three.City.Name
                                                   }).ToList().ForEach(this.Weather.Add);
                        break;
                    case 7:
                        var seven = JsonConvert.DeserializeObject<Seven>(
                            await client.GetStringAsync("http://localhost:31657/Home/GetWeatherSevenDaysJson?city=" + city));
                        this.Weather.Clear();
                        seven.List.Select(w => new Weather
                                                   {
                                                       Max = w.Temp.Max,
                                                       Min = w.Temp.Min,
                                                       Date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(w.Dt).Date.ToString("D"),
                                                       Icon = $"http://openweathermap.org/img/w/{w.Weather[0].Icon}.png",
                                                       City = seven.City.Name
                                                   }).ToList().ForEach(this.Weather.Add);
                        break;
                }
            }
        }
    }
}
