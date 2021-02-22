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

        public Task<int> ChangeUserCredential(int userId, int categoryId, string login, string password);
    }
}
