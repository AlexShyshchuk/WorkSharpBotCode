using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace NewBot.Commands
{

    [Serializable]

    public class ToUpper : ITools
    {
        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }

        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                if (!String.IsNullOrEmpty(activity.Text)){
                    activity.Text = activity.Text.ToUpper();
                    context.Done(activity);
                }
                else{
                    await context.PostAsync("Введи текст");
                    context.Wait(Run);
                }
            }
        }

        public ToUpper()
        {
            CommandsName = new List<string>() { "/toupper" };
            Description = "Приводит к верхнему реистру";
        }

        public async Task StartAsync(IDialogContext context){
            context.Wait(Run);
        }

    }
}