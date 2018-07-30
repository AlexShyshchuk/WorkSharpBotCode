using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using NewBot.Commands;
using System.Reflection;
using NewBot.Models;
using System.Threading;
using Microsoft.Bot.Builder.Dialogs;

namespace NewBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            DataModel.RememberUser(activity);
            if (!String.IsNullOrEmpty(activity?.Text))
            {
                var str = activity.Text.Trim();
                var indexOfSpase = str.IndexOf(" ", StringComparison.Ordinal);
                var command = indexOfSpase != -1 ? str.Substring(0, indexOfSpase).ToLower() : str.ToLower();

                var _tools = new List<ITools>();

                var baseInterfaceType = typeof(ITools);
                var botCommand = Assembly.GetAssembly(baseInterfaceType).GetTypes().Where(types => types.IsClass && !types.IsAbstract && types.GetInterface("ITools") != null);
                foreach (var BotCommand in botCommand)
                {
                    _tools.Add((ITools)Activator.CreateInstance(BotCommand));
                }


                var tools = new List<ITools>();

                var tool = _tools.FirstOrDefault(x => x.CommandsName.Any(y => y.Equals(command)));

                if (tool != null)
                {

                    activity.Text = indexOfSpase >= 0 ? activity.Text.Substring(indexOfSpase, str.Length - indexOfSpase) : String.Empty;

                    var typeOfDialog = tool.GetType();
                    var newTool = Activator.CreateInstance(typeOfDialog);
                    await context.Forward((IDialog<object>)newTool, ResumeAfterNewOlderDialog, activity, CancellationToken.None);

                }
                else
                {
                    await context.Forward(new Help(), ResumeAfterNewOlderDialog, activity, CancellationToken.None);
                }
            }

        }

        private async Task ResumeAfterNewOlderDialog (IDialogContext context, IAwaitable<object> result){

            var activity = await result as Activity;
            var reply = activity.CreateReply(activity.Text);
            if (activity != null) {
                
                if (activity.Attachments != null) {
                    reply.Attachments = activity.Attachments;
                }
            }
            await context.PostAsync(reply);

        }

    }
}