using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface IUserCredentialService
    {
        public Task<List<UserCredential>> GetAllUserCredentials();
    }
}
