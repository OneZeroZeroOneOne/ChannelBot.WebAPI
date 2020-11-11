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

    }
}
