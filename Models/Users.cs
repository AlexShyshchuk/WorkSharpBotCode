using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace NewBot.Models
{
    public class User
    {
        
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string ChanalId { get; set; }
        public string ServiseUrl { get; set; }
        public string Conversation { get; set; }

        public User (Activity activity)
        {
            if (activity != null)
            {
                UserId = activity.From.Id;
                UserName = activity.From.Name;
                FromId = activity.Recipient.Id;
                FromName = activity.Recipient.Name;
                ChanalId = activity.ChannelId;
                Conversation = activity.Id;
            }
        }

    }
}