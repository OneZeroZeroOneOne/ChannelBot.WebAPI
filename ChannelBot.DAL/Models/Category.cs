﻿using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            Group = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Lang { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual UserCredential UserCredential { get; set; }
        public virtual ICollection<Group> Group { get; set; }
    }
}
