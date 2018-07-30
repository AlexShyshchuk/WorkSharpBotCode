using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;


namespace NewBot.Commands
{
    [Serializable]
    public class SendForAll : ITools
    {
        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }

        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                if (!String.IsNullOrEmpty(activity.Text))
                {
                    BotServise.SendForAll(activity.Text);
                    activity.Text = "Сообщение отправленно";
                    context.Done(activity);
                }
                else
                {
                    await context.PostAsync("Введи текст");
                    context.Wait(Run);
                }
            }

        }
        public SendForAll()
        {
            CommandsName = new List<string>() { "/sendforall" };
            Description = "Отправить сообщение всем пользователям";
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Run);
        }
    }
}
