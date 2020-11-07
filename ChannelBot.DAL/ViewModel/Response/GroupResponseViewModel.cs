using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.Response
{
    public class GroupResponseViewModel
    {
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public virtual List<SourceResponseViewModel> Sources { get; set; }
    }
}
