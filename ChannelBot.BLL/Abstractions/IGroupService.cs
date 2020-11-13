using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface IGroupService
    {
        public Task<Group> GetGroup(int id);

        public Task CreateGroup(int id);

        public Task<List<Group>> GetAllGroups();
        public Task AddSource(int groupId, int sourceId);

        public Task<List<Source>> GroupSource(int groupId);

        public Task DeleteGroup(int sourceId);
    }
}
