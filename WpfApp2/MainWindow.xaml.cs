using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _cities;
        private WeatherInfo _weatherInfo;
        public MainWindow()
        {
            InitializeComponent();
            _cities = new List<string> { "Warsaw", "Berlin", "Copenhagen","Athens","Madrid","Helsinki","Budapest","Brussels","Amsterdam","Dublin","Paris","Lisbon",
            "Rome","Vienna","Bern","London","Nicosia","Sofia","Stockholm","Tirana","Luxembourg"};
            comboBoxCity.ItemsSource = _cities;
            comboBoxCity.SelectedIndex = 0;
            _weatherInfo = new WeatherInfo();
        }

        private async void buttonGetWeather_Click(object sender, RoutedEventArgs e)
        {
            WeatherData? weatherData = await _weatherInfo.GetWeather(comboBoxCity.Text);
            if (weatherData != null)
            {
                textBlockInfo.Text =($"Opis: {weatherData?.Weather[0].Description}\n");
                textBlockInfo.Text += ($"Temperatura: {weatherData.Main.Temp}\n");
                textBlockInfo.Text += ($"Ciśnienie: {weatherData.Main.Pressure}\n");
                textBlockInfo.Text += ($"Wilgotność: {weatherData.Main.Humidity}");
                imageWeather.Source = new BitmapImage(new Uri(_weatherInfo.ImageLink));
            }
            else
            {
                textBlockInfo.Text = "Błąd pobierania danych";
            }
            //imageWeather.Source = new BitmapImage(new Uri("http://openweathermap.org/img/w/10d.png"));
        }
    }
}