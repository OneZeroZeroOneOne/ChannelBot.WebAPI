using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface ISourceService
    {
        public Task<Source> GetSource(int id);

        public Task<List<Source>> GetAllSource();

        public Task CreateSource(string Url, int platformId);

        
    }
}
