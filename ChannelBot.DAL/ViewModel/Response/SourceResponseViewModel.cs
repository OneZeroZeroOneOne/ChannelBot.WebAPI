using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.Response
{
    public class SourceResponseViewModel
    {
        public string SourceUrl { get; set; }
        public int Id { get; set; }
        public int? PlatformId { get; set; }

        //public virtual ICollection<Content> Content { get; set; }
        //public virtual ICollection<GroupSource> GroupSource { get; set; }
    }
}
