using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using NewBot.Interfaces;
using NewBot.Commands;
using System.Reflection;
using NewBot.Models;

namespace NewBot
{
    public class BotServise
    {

        public static void SendForUser(User user, string message)
        {
            if(user != null)
            {
                var client = new ConnectorClient(new Uri(user.ServiseUrl));

                var userAccount = new ChannelAccount(user.UserId, user.UserName);
                var botAccount = new ChannelAccount(user.FromId, user.FromName);

                var activity = new Activity
                {
                    Conversation = new ConversationAccount(id: user.UserId),
                    ChannelId = user.ChanalId,
                    From = userAccount,
                    Recipient = botAccount,
                    Text = message,
                    Id = user.Conversation
                };

                var reply = activity.CreateReply(message);
                if (!String.IsNullOrEmpty(activity.ServiceUrl))
                {
                    MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
                }
                MicrosoftAppCredentials.TrustServiceUrl(user.ServiseUrl);

                client.Conversations.ReplyToActivity(reply);

            }
        }
        public static void SendForAll(string message)
        {
            foreach(var user in DataModel.Users)
            {
                SendForUser(user, message);
            }
        }
    }

}