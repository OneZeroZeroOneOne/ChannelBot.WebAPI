using System;
using System.Collections.Generic;

namespace ChannelBot.DAL.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Bot = new HashSet<Bot>();
            Category = new HashSet<Category>();
            Channel = new HashSet<Channel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Bot> Bot { get; set; }
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Channel> Channel { get; set; }
    }
}
