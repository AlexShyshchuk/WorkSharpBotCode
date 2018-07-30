using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using WeatherInformation;
using System.Text;
using System.Net;
using WeatherCli;
using System.Xml.Linq;
using System.IO;
using System.Device.Location;


namespace NewBot.Commands
{

    [Serializable]

    public class WeatherAnswer : ITools
    {

        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }

        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                //double x, y;
                //var geoLocation = new GeoCoordinate();

                //x = geoLocation.Latitude;
                //y = geoLocation.Longitude;

                var answer = new WeatherInform();

                string text = answer.getInfo();


                await context.PostAsync(text);
                context.Done(activity);
            }

        }
        public WeatherAnswer()
        {
            CommandsName = new List<string>() { "/weather" };
            Description = "Получить информацию о погоде";
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Run);
        }

    }
}