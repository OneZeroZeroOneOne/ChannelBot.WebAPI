using Microsoft.AspNetCore.Authorization;

namespace ChannelBot.Authorization.Bll
{
    public class RoleEntryRequirement : IAuthorizationRequirement
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private string RoleName { get; }

        public RoleEntryRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
