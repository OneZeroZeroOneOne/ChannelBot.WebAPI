using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ChannelBot.BLL.Abstractions;
using System.Security.Cryptography.X509Certificates;
using ChannelBot.Utilities.Exceptions;
using System.Runtime.CompilerServices;

namespace ChannelBot.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MainContext _context;
        public CategoryService(MainContext context)
        {
            _context = context;
        }

        async public Task<List<Category>> GetAllCategories(int userId)
        {
            return await _context.Category.Include(x => x.UserCredential).Include(x => x.Group).ThenInclude(x => x.GroupSource).ThenInclude(x => x.Source).Where(x => x.AdminId == userId).ToListAsync();
            
        }

        async public Task<Category> GetCategory(int id, int userI)
        {
            return await _context.Category.Include(x => x.Group).ThenInclude(x => x.GroupSource).ThenInclude(x => x.Source).FirstOrDefaultAsync(y => y.Id == id && y.AdminId == userI);
            
        }

        async public Task CreateCategory(string title, int userI)
        {
            Category c = new Category();
            c.Title = title;
            c.AdminId = userI;
            await _context.AddAsync(c);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteCategory(int id, int userI)
        {
            Category c = await _context.Category.FirstOrDefaultAsync(x => x.AdminId == userI && x.Id == id);
            if(c != null)
            {
                _context.UserCredential.RemoveRange(_context.UserCredential.Where(x => x.CategoryId == id));
                _context.GroupSource.RemoveRange(_context.GroupSource.Include(x => x.Group).Where(x => x.Group.CategoryId == id));
                _context.Group.RemoveRange(_context.Group.Include(x => x.Category).Where(x => x.CategoryId == id));
                _context.Category.RemoveRange(_context.Category.Where(x => x.Id == id && x.AdminId == userI));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.CategoryNotFound, $"Category with id {id} not found");
            }
            
        }

    }
}
