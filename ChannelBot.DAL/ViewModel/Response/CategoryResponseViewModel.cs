using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.Response
{
    public class CategoryResponseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual UserCredentialResponseViewModel UserCredential { get; set; }
        public List<GroupResponseViewModel> Groups { get; set; }
    }
}
