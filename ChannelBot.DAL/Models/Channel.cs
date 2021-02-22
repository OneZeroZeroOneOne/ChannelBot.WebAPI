using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Channel
    {
        public Channel()
        {
            ChannelGroup = new HashSet<ChannelGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AdminId { get; set; }
        public int BotId { get; set; }
        public int TelegramId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Bot Bot { get; set; }
        public virtual ICollection<ChannelGroup> ChannelGroup { get; set; }
    }
}
