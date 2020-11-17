using System;
using System.Collections.Generic;

namespace ChannelBot.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
