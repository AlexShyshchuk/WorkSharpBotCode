using System;
using System.Collections.Generic;
using System.IO;
using WeatherCli;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Translator;
using Newtonsoft.Json;

namespace WeatherInformation
{
    class WeatherInform
    {
        public string getInfo()
        {
            
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Kiev&units=metric&appid=7a395325d2a8a24ad7fc7f41b1386573";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
            string cityName = weatherResponse.name;
            string airTemperature = weatherResponse.main.temp.ToString();
            string weatherDescription = weatherResponse.weather[0].description;
            string windSpeed = weatherResponse.wind.speed.ToString();

            /*var translete = new YandexTranslator();

            cityName = translete.Translate(cityName);
            weatherDescription = translete.Translate(weatherDescription);*/

            var str = new StringBuilder();

            str.Append($"# Weather in {cityName}:\n\r");
            str.Append("![Изображение](http://archive.krakow2016.com/media/cache/resolve/filemanager_original/images/7147/NEWS_PHOTO_weather.png)\n\r");
            str.Append($"{weatherDescription},\n\r");
            str.Append($"temperature of air: {airTemperature} °C,\n\r");
            str.Append($"wind speed: {windSpeed} м/с.\n\r");

            return str.ToString();

        }
    }
}
