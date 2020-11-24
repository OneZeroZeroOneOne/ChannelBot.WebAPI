using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Services
{
    public class UserCredentialService: IUserCredentialService
    {
        private readonly MainContext _context;
        public UserCredentialService(MainContext context)
        {
            _context = context;
        }

        public Task<List<UserCredential>> GetAllUserCredentials()
        {
            return _context.UserCredential.ToListAsync();
        }
    }
}
