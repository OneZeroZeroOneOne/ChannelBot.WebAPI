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
    public class GroupService: IGroupService
    {
        private readonly MainContext _context;
        public GroupService(MainContext context)
        {
            _context = context;
        }

        async public Task CreateGroup(int categoryId, int serialNumber)
        {
            Group g = new Group();
            g.CategoryId = categoryId;
            g.SerialNumber = serialNumber;
            await _context.Group.AddAsync(g);
            await _context.SaveChangesAsync();
        }

        async public Task<List<Group>> GetAllGroups()
        {
            return await _context.Group.Include(x => x.GroupSource).ThenInclude(x => x.Source).ToListAsync();
        }

        async public Task<Group> GetGroup(int id)
        {
            return await _context.Group.Include(x => x.GroupSource).ThenInclude(x => x.Source).FirstOrDefaultAsync(x => x.Id == id);
        }

        async public Task AddSource(int groupId, int sourceId)
        {
            await _context.GroupSource.AddAsync(new GroupSource()
            {
                GroupId = groupId,
                SourceId = sourceId,
            });
            await _context.SaveChangesAsync();
        }

        async public Task<List<Source>> GroupSource(int groupId)
        {
            var groupSource = _context.GroupSource.Include(x => x.Source).Where(x => x.GroupId == groupId);
            return await groupSource.Select(x => x.Source).ToListAsync();
        }

        async public Task DeleteGroup(int groupId)
        {
            _context.GroupSource.RemoveRange(_context.GroupSource.Where(x => x.GroupId == groupId));
            _context.Group.RemoveRange(_context.Group.Where(x => x.Id == groupId));
            await _context.SaveChangesAsync();
        }
    }
}
