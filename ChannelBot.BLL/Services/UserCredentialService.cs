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

        public async Task<int> ChangeUserCredential(int userId, int categoryId, string login, string password)
        {
            UserCredential userCredential = await _context.UserCredential.Include(x => x.Category).FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.Category.AdminId == userId);
            if(userCredential == null)
            {
                await _context.UserCredential.AddAsync(new UserCredential()
                {
                    CategoryId = categoryId,
                    UserName = login,
                    UserPassword = password
                });
            }
            else
            {
                userCredential.UserPassword = password;
                userCredential.UserName = login;
            }
            await _context.SaveChangesAsync();
            return 0;


        }

        public async Task<List<UserCredential>> GetAllUserCredentials()
        {
            return await _context.UserCredential.ToListAsync();
        }
    }
}
