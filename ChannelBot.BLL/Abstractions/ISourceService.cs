using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface ISourceService
    {
        public Task<Source> GetSource(int id, int userId);

        public Task<List<Source>> GetAllSource(int userId);

        public Task<int> CreateSource(string Url, int platformId, int userId);

        public Task DeleteSource(int sourceId, int userId);



    }
}
