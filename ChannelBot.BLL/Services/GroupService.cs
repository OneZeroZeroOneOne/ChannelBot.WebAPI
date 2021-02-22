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
    public class GroupService: IGroupService
    {
        private readonly MainContext _context;
        public GroupService(MainContext context)
        {
            _context = context;
        }

        async public Task CreateGroup(int categoryId, int serialNumber, int userId)
        {
            Group g = new Group();
            g.CategoryId = categoryId;
            g.SerialNumber = serialNumber;
            await _context.Group.AddAsync(g);
            await _context.SaveChangesAsync();
        }

        async public Task<List<Group>> GetAllGroups(int userId)
        {
            return await _context.Group.Include(x => x.Category).Include(x => x.GroupSource).ThenInclude(x => x.Source).Where(x => x.Category.AdminId == userId).ToListAsync();
        }

        async public Task<Group> GetGroup(int id, int userId)
        {
            return await _context.Group.Include(x => x.Category).Include(x => x.GroupSource).ThenInclude(x => x.Source).FirstOrDefaultAsync(x => x.Id == id && x.Category.AdminId == userId);
        }

        async public Task AddSource(int groupId, int sourceId, int userId)
        {
            Group g = await _context.Group.Include(x => x.Category).FirstOrDefaultAsync(x => x.Category.AdminId == userId && x.Id == groupId);
            Source s = await _context.Source.FirstOrDefaultAsync(x => x.Id == sourceId);
            if (g != null && s != null)
            {
                await _context.GroupSource.AddAsync(new GroupSource()
                {
                    GroupId = groupId,
                    SourceId = sourceId,
                });
                await _context.SaveChangesAsync();
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.GroupNotFound, $"Group with id {groupId} or source with {sourceId} not found");
            }
            
        }

        async public Task<List<Source>> GroupSource(int groupId, int userId)
        {
            Group g = await _context.Group.Include(x => x.Category).FirstOrDefaultAsync(x => x.Category.AdminId == userId && x.Id == groupId);
            if (g != null)
            {
                var groupSource = _context.GroupSource.Include(x => x.Source).Where(x => x.GroupId == groupId);
                return await groupSource.Select(x => x.Source).ToListAsync();
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.GroupNotFound, $"Group with id {groupId} not found");
            }
            
        }

        async public Task DeleteGroup(int groupId, int userId)
        {
            Group g = await _context.Group.Include(x => x.Category).FirstOrDefaultAsync(x => x.Category.AdminId == userId && x.Id == groupId);
            if(g != null)
            {
                _context.GroupSource.RemoveRange(_context.GroupSource.Where(x => x.GroupId == groupId));
                _context.Group.RemoveRange(_context.Group.Where(x => x.Id == groupId));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.GroupNotFound, $"Group with id {groupId} not found");
            }
        }
    }
}
