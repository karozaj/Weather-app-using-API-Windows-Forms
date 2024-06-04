using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

public class WeatherInfo
{
    private string _apiKey= "";
    private string _url="https://api.openweathermap.org/data/2.5/weather";
    private string _imageUrl="http://openweathermap.org/img/w/";
    public string ImageLink { get; set; }
    private readonly HttpClient _httpClient;

    public WeatherInfo()
    {
        
        _httpClient = new HttpClient();
    }

    public async Task<WeatherData?> GetWeather(string city)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{this._url}?q={city}&appid={this._apiKey}&units=metric&lang=pl");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            //WeatherData weatherData = new WeatherData();
            //weatherData.response = responseBody;
            WeatherData? weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
            ImageLink = _imageUrl + weatherData.Weather[0].Icon + ".png";
            Console.WriteLine(ImageLink);
            return weatherData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }


}

public class WeatherData
{
    public WeatherMain? Main { get; set; }
    public WeatherDesc[]? Weather { get; set; }
    //public string response { get; set; }
}

public class WeatherDesc
{
    public string? Description { get; set; }
    public string? Icon { get; set; }
}

public class WeatherMain
{
    public float Temp { get; set; }
    public float Pressure { get; set; }
    public float Humidity { get; set; }
}