using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using System.Text;

namespace NewBot.Commands
{

    [Serializable]

    public class NewGuid : ITools
    {

        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }

        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                await context.PostAsync(Guid.NewGuid().ToString());
                context.Done(activity);
            }

        }
        public NewGuid()
        {
            CommandsName = new List<string>() { "/newguid" };
            Description = "Получить уникальный индефикатор";
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Run);
        }
    }
}