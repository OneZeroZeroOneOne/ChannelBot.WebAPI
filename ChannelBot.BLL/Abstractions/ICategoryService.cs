using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories(int userId);
        public Task<Category> GetCategory(int id, int userId);

        public Task CreateCategory(string title, int userId);

        public Task DeleteCategory(int id, int userId);
    }
}
