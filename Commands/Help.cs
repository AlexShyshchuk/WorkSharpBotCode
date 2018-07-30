using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection ;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace NewBot.Commands
{

    [Serializable]

    public class Help : ITools
    {
        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }

        protected string CommandText { get; set; }

        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                await context.PostAsync(CommandText);
                context.Done(activity);
            }

        }

        public Help()
        {
            CommandsName = new List<string>() { "/help" };
            Description = "Помощь";

            var result = new StringBuilder();
            var baseInterfaceType = typeof(ITools);
            var botCommands = Assembly.GetAssembly(baseInterfaceType).GetTypes()
                                      .Where(types => types.IsClass && !types.IsAbstract && 
                                             types.GetInterface("ITools") != null && types.Name != "Help" && types.Name != "Cards" 
                                             && types.Name!= "SendForAll");

            foreach (var botCommand in botCommands) {
                var command = (ITools)Activator.CreateInstance(botCommand);
                result.Append($"{command.CommandsName.First()} - {command.Description}\n\r");
            }
            CommandText = result.ToString();
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Run);
        }

    }
}