using System.Security.Principal;

namespace ChannelBot.Authorization.Bll
{
    public class AuthorizedUserModel : IIdentity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string? AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public string? Name { get; }
    }
}
