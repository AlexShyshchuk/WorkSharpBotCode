using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace NewBot.Commands
{

    [Serializable]

    public class Markdown : ITools
    {

        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }

        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                var str = new StringBuilder();
                str.Append("**Жирный текст**\n\r");
                str.Append("*Курсив*\n\r");
                str.Append("# Заголовок\n\r");
                str.Append("~~Перечёркнутый текст~~\n\r");
                str.Append("---\n\r");
                str.Append("* Элемент не сортированого списка\n\r");
                str.Append("1. Элемент сортированого списка\n\r");
                str.Append("`Переформатированный список`\n\r");
                str.Append("> Цитата\n\r");
                str.Append("[Ссылка](https://dev.botframework.com)\n\r");
                str.Append("![Изображение](http://ona-znaet.ru/statii/1/61/d3577.jpg)\n\r");
                await context.PostAsync(str.ToString());
                context.Done(activity);
            }

        }

        public Markdown()
        {
            CommandsName = new List<string>() { "/markdown" };
            Description = "Выводит все доступные оформление текста";
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Run);
        }

    }
}