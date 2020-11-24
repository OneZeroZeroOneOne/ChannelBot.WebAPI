using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.Response
{
    public class UserCredentialResponseViewModel
    {
        public int CategoryId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int PlatformId { get; set; }
    }
}
