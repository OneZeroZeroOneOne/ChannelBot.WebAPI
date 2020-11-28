using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.In
{
    public class CreateContentInModel
    {
        public long id { get; set; }
        public string mediaUrl { get; set; }
        public string description { get; set; }
        public int sourceId { get; set; }
    }
}
