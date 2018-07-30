using System;
namespace WeatherCli
{
    public class WeatherResponse
    {
        public Weather[] weather { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public string name { get; set; }

    }
}
