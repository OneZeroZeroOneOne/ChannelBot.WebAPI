using ChannelBot.DAL.Models;
using ChannelBot.DAL.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface IContentService
    {
        public Task<List<Content>> GetAllContent();

        public Task<Content> GetContent(int id);

        public Task CreateContent(long id, string mediaUrl, string description, int sourceId);
    }
}
