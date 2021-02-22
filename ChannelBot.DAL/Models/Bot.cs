using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Bot
    {
        public Bot()
        {
            Channel = new HashSet<Channel>();
        }

        public string Token { get; set; }
        public int AdminId { get; set; }
        public int TelegramId { get; set; }
        public int Id { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual ICollection<Channel> Channel { get; set; }
    }
}
