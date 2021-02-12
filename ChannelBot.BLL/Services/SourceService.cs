using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Services
{
    public class SourceService: ISourceService
    {
        private readonly MainContext _context;
        public SourceService(MainContext context)
        {
            _context = context;
        }

        async public Task<Source> GetSource(int id)
        {
            return await _context.Source.Include(x => x.GroupSource).ThenInclude(x => x.Group).FirstOrDefaultAsync(x => x.Id == id);
        }


        async public Task<int> CreateSource(string Url, int platformId)
        {
            Source s = new Source();
            s.SourceUrl = Url;
            s.PlatformId = platformId;
            await _context.Source.AddAsync(s);
            await _context.SaveChangesAsync();
            return s.Id;
        }

        async public Task<List<Source>> GetAllSource()
        {
            return await _context.Source.Include(x => x.GroupSource).ThenInclude(x => x.Group).ToListAsync();
        }

        async public Task DeleteSource(int sourceId)
        {
            _context.GroupSource.RemoveRange(_context.GroupSource.Where(x => x.SourceId == sourceId));
            _context.Source.RemoveRange(_context.Source.Where(x => x.Id == sourceId));
            await _context.SaveChangesAsync();
        }
    }
}
