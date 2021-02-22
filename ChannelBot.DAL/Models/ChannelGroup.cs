using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class ChannelGroup
    {
        public int ChannelId { get; set; }
        public int GroupId { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual Group Group { get; set; }
    }
}
