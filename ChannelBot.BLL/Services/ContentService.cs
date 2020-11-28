using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Services
{
    public class ContentService : IContentService
    {
        private readonly MainContext _context;
        public ContentService(MainContext context)
        {
            _context = context;
        }

        public async Task CreateContent(int id, string mediaUrl, string description, int sourceId)
        {
            Content content = new Content()
            {
                Id = id,
                MediaUrl = mediaUrl,
                SourceId = sourceId,
                Description = description,
            };
            await _context.Content.AddAsync(content);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Content>> GetAllContent()
        {
            return await _context.Content.Include(x => x.Source).ToListAsync();
        }

        public async Task<Content> GetContent(int id)
        {
            return await _context.Content.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
