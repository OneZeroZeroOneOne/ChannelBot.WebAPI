using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.Response
{
    public class ContentResponseViewModel
    {
        public long Id { get; set; }
        public string MediaUrl { get; set; }
        public string Description { get; set; }
        public int SourceId { get; set; }
    }
}
