using ChannelBot.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface IGroupService
    {
        public Task<Group> GetGroup(int id, int userId);

        public Task CreateGroup(int id, int serialNumber, int userId);

        public Task<List<Group>> GetAllGroups(int userId);
        public Task AddSource(int groupId, int sourceId, int userId);

        public Task<List<Source>> GroupSource(int groupId, int userId);

        public Task DeleteGroup(int sourceId, int userId);
    }
}
