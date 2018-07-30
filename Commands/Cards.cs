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
using NewBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;

namespace NewBot.Commands
{

    [Serializable]


    public class Cards : ITools
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
                switch (command)
                {

                    case HeroCard:
                        attachment = GetHeroCard();
                        break;
                    case ThumbnailCard:
                        attachment = GetThumbnailCard();
                        break;
                    case ReceiptCard:
                        attachment = GetReceiptCard();
                        break;
                    case SigninCard:
                        attachment = GetSigninCard();
                        break;
                    case AnimationCard:
                        attachment = GetAnimationCard();
                        break;
                    case VideoCard:
                        attachment = GetVideoCard();
                        break;
                    case AudioCard:
                        attachment = GetAudioCard();
                        break;
                    default:
                        attachment = GetDefaultCard();
                        break;

                }

                activity.Attachments.Add(attachment);


                await context.PostAsync(activity.CreateReply());

                context.Done(activity);
            }

        }


        public Cards()
        {
            CommandsName = new List<string>() { "/cards" };
            Description = "Отправляет все типы карточек";
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
        //1
        private static Attachment GetHeroCard(){

            var heroCard = new HeroCard()
            {
                Title = "HeroCard",
                Subtitle = "Содержит одно большое изображение, одну или несколько кнопок и текст.",
                Text = "Текст для этой карточки",
                Images = new List<CardImage> { new CardImage("https://cdn.newsapi.com.au/image/v1/0cb8e958668d3829c5206d86b3bb421a") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Открыть ссылку", value: "https://dev.botframework.com") }

            };

            return heroCard.ToAttachment();
        }
        //2
        private static Attachment GetThumbnailCard()
        {

            var heroCard = new ThumbnailCard()
            {
                Title = "Thumbnail Card",
                Subtitle = "Содержит одно большое изображение, одну или несколько кнопок и текст.",
                Text = "Текст для этой карточки",
                Images = new List<CardImage> { new CardImage("https://www.washingtonpost.com/resizer/78WcfDo4SA85KacdlVpM_xDffsk=/480x0/arc-anglerfish-washpost-prod-washpost.s3.amazonaws.com/public/7P55T6BXJIZD5EB5O3VQOGLQVM.png") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Открыть ссылку", value: "https://azure.microsoft.com/ru-ru/") }

            };

            return heroCard.ToAttachment();
        }
        //3
        private static Attachment GetReceiptCard()
        {

            var heroCard = new ReceiptCard()
            {
                Title = "Receipt Card",
                Facts = new List<Fact> { new Fact("Номер заказа", "34221543"), new Fact("Метод оплаты", "VISA 1234-****-****") },
                Items = new List<ReceiptItem>
                {
                    new ReceiptItem("Обмен данными", price: "$ 10.50", quantity: "388", image: new CardImage(url: "https://github.com/amido/azure-vector-icons/blob/master/icons/Azure%20Active%20Directory%20(AAD).svg")),
                    new ReceiptItem("Sluzhba prilozheniy", price: "$ 100.00", quantity:"1000", image: new CardImage(url:"https://github.com/amido/azure-vector-icons/blob/master/icons/Azure%20Active%20Directory%20Access%20Control%20Services%20(ACS).svg"))
                },
                Tax = "$ 5.50",
                Total = "$ 116.00",
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, "More info", 
                                   "http://geek-nose.com/wp-content/uploads/2016/01/kak-ustanovit-prilozhenie-na-android-s-kompjutera-№12.jpg" ,
                                   "https://dev.botframework.com")
                }

            };

            return heroCard.ToAttachment();
        }
        //4
        private static Attachment GetSigninCard()
        {

            var heroCard = new SigninCard()
            {
                Text = "Signin Card",

                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Log in", value: "https://uk-ua.facebook.com/login/") }

            };

            return heroCard.ToAttachment();
        }
        //5
        private static Attachment GetAnimationCard()
        {

            var heroCard = new AnimationCard()
            {
                Title = "Animation Card",
                Subtitle = "Может воспроизводить гифки или короткие видеоролики",
                Media = new List<MediaUrl> {
                    new MediaUrl(){
                        Url = "https://78.media.tumblr.com/584f14d9c738bcdf6fa50017a527d17c/tumblr_oob96eVhMi1vh1yxpo1_500.gif"
                    }
                }
            };

            return heroCard.ToAttachment();
        }
        //6
        private static Attachment GetVideoCard()
        {

            var heroCard = new VideoCard()
            {
                Title = "VideoCard",
                Subtitle = "Может воспроизводить видео",
                Text = "Текст видео",
                Image = new ThumbnailUrl
                {
                    Url = "https://scontent-lga3-1.cdninstagram.com/vp/831d2e6d09e74ca73d9f12ce5cbfe8a1/5BE9E8D1/t51.2885-15/e35/p320x320/26868302_155746251873719_748056957910253568_n.jpg?ig_cache_key=MTY5ODc2NjU4NjgwODEyODUwMQ%3D%3D.2"
                },
                Media = new List<MediaUrl> {
                    new MediaUrl(){
                        Url = "https://youtu.be/31j4DIpgY9U"
                    }
                }


            };

            return heroCard.ToAttachment();
        }
        //7
        private static Attachment GetAudioCard()
        {

            var heroCard = new AudioCard()
            {
                Title = "Audio Card",
                Subtitle = "Может возпроизводить аудиофайл",
                Text = "Текст этой карточки",
                Image = new ThumbnailUrl {
                    Url = "http://mindequalsblown.net/wp-content/uploads/2014/06/Rock-Star.jpg"
                },
                Media = new List<MediaUrl>{
                    new MediaUrl(){
                        Url = "http://uznavo.net/mp3/zarubezhnye-novinki/Post_Malone_feat._21_Savage_-_Rockstar_(UzNAVO.NET).mp3"
                    }
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