using System.Security.Claims;

namespace YuGiOhTeamApp.Services
{
    public interface IUserContextService
    {
        Guid? GetUserId { get; }
        ClaimsPrincipal User { get; }
        bool IsLeader { get; }
    }
}