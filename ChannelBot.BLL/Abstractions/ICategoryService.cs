using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategory(int id);

        public Task CreateCategory(string title);
    }
}
