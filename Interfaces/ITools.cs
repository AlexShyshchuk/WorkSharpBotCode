using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace NewBot.Interfaces
{
    interface ITools : IDialog<object>
    {
        string Description { get; set; }

        List<string> CommandsName { get; set; }

        bool IsAdmin { get; set; }

        Task Run(IDialogContext context, IAwaitable<IMessageActivity> result);
        
    }
}
