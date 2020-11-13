using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ChannelBot.BLL.Abstractions;

namespace ChannelBot.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private MainContext _context;
        public CategoryService(MainContext context)
        {
            _context = context;
        }

        async public Task<List<Category>> GetAllCategories()
        {
            List<Category>  l = await _context.Category.Include(x => x.Group).ThenInclude(x => x.GroupSource).ThenInclude(x => x.Source).ToListAsync();
            return l;
        }

        async public Task<Category> GetCategory(int id)
        {
            Category l = await _context.Category.Include(x => x.Group).ThenInclude(x => x.GroupSource).ThenInclude(x => x.Source).FirstOrDefaultAsync(y => y.Id == id);
            return l;
        }

        async public Task CreateCategory(string title)
        {
            Category c = new Category();
            c.Title = title;
            await _context.AddAsync(c);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteCategory(int id)
        {
            _context.UserCredential.RemoveRange(_context.UserCredential.Where(x => x.CategoryId == id));
            _context.GroupSource.RemoveRange(_context.GroupSource.Include(x => x.Group).Where(x => x.Group.CategoryId == id));
            _context.Group.RemoveRange(_context.Group.Include(x => x.Category).Where(x => x.CategoryId == id));
            _context.Category.RemoveRange(_context.Category.Where(x => x.Id == id));
            await _context.SaveChangesAsync();
        }

    }
}
