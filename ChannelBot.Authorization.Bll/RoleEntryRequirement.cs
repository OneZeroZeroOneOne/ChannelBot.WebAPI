using Microsoft.AspNetCore.Authorization;

namespace ChannelBot.Authorization.Bll
{
    public class RoleEntryRequirement : IAuthorizationRequirement
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int RoleId { get; }

        public RoleEntryRequirement(int roleId)
        {
            RoleId = roleId;
        }
    }
}
