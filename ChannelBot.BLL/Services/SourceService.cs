using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using ChannelBot.Utilities.Exceptions;
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

        async public Task<Source> GetSource(int id, int userId)
        {
            return await _context.Source.Include(x => x.GroupSource).ThenInclude(x => x.Group).FirstOrDefaultAsync(x => x.Id == id && x.AdminId == userId);
        }


        async public Task<int> CreateSource(string Url, int platformId, int userId)
        {
            Source s = new Source();
            s.SourceUrl = Url;
            s.PlatformId = platformId;
            s.AdminId = userId;
            await _context.Source.AddAsync(s);
            await _context.SaveChangesAsync();
            return s.Id;
        }

        async public Task<List<Source>> GetAllSource(int userId)
        {
            return await _context.Source.Include(x => x.GroupSource).ThenInclude(x => x.Group).Where(x => x.AdminId == userId).ToListAsync();
        }

        async public Task DeleteSource(int sourceId, int userId)
        {
            Source s = await _context.Source.FirstOrDefaultAsync(x => x.AdminId == userId && x.Id == sourceId);
            if(s != null)
            {
                _context.GroupSource.RemoveRange(_context.GroupSource.Where(x => x.SourceId == sourceId));
                _context.Source.RemoveRange(_context.Source.Where(x => x.Id == sourceId));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.SourceNotFound, $"Source with id {sourceId} not found");
            }
        }
    }
}
