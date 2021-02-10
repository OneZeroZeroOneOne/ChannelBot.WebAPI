using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ChannelBot.Authorization.Bll
{
    public class RoleEntryHandler : AuthorizationHandler<RoleEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleEntryRequirement requirement)
        {
            if (context.User.Identity.GetType() == typeof(AuthorizedUserModel))
            {
                var myUser = (AuthorizedUserModel)context.User.Identity;
                if (myUser != null)
                {
                    if (myUser.RoleId == requirement.RoleId)
                    {
                        context.Succeed(requirement);
                    }
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
