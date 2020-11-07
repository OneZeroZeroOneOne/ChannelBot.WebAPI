﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.DAL.ViewModel.Response
{
    public class CategoryResponseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //public virtual UserCredential UserCredential { get; set; }
        public virtual List<GroupResponseViewModel> Groups { get; set; }
    }
}