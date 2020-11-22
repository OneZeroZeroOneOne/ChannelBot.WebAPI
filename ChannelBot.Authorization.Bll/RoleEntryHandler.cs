using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ChannelBot.Authorization.Bll
{
    public class RoleEntryHandler : AuthorizationHandler<RoleEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleEntryRequirement requirement)
        {
            var authorizedUser = (AuthorizedUserModel)context.User.Identity;
            if (authorizedUser.RoleId == 1)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
