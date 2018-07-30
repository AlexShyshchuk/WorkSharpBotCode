using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace NewBot.Commands
{
    [Serializable]

    public class DefaultCard : ITools
    {

        public string Description { get; set; }

        public List<string> CommandsName { get; set; }

        public bool IsAdmin { get; set; }


        private const string HeroCard = "hero";
        private const string ThumbnailCard = "thumbnail";
        private const string ReceiptCard = "receipt";
        private const string SigninCard = "sign-in";
        private const string AnimationCard = "animation";
        private const string VideoCard = "video";
        private const string AudioCard = "audio";


        public async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            if (activity?.Conversation != null)
            {
                
                activity.Attachments = new List<Attachment>();
                var command = activity.Text.Trim().ToLower();

                Attachment attachment;

                attachment = GetDefaultCard();

                activity.Attachments.Add(attachment);

                await context.PostAsync(activity.CreateReply());

                context.Done(activity);
            }
        }

        public DefaultCard()
        {
            CommandsName = new List<string>() { "/card" };
            Description = "Список доступных карточек";
        }
        //0
        private static Attachment GetDefaultCard()
        {

            var heroCard = new HeroCard()
            {
                Title = "Types of cards",

                Buttons = new List<CardAction>
                {

                    new CardAction(ActionTypes.ImBack, "HeroCard", value: "/cards " + HeroCard),
                    new CardAction(ActionTypes.ImBack, "Tumbnail", value: "/cards " + ThumbnailCard),
                    new CardAction(ActionTypes.ImBack, "Receipt", value: "/cards " + ReceiptCard),
                    new CardAction(ActionTypes.ImBack, "Signin", value: "/cards " + SigninCard),
                    new CardAction(ActionTypes.ImBack, "Animation", value: "/cards " + AnimationCard),
                    new CardAction(ActionTypes.ImBack, "Video", value: "/cards " + VideoCard),
                    new CardAction(ActionTypes.ImBack, "Audio", value: "/cards " + AudioCard),

                }
            };

            return heroCard.ToAttachment();

        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(Run);
        }


    }
}