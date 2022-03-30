using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface ITeamService
    {
        bool ChangeDescription(string newDesc);
        void CreateTeam(CreateTeamDto dto);
        void PassLeader(string username);
        void DeleteTeam();
        string RequestToJoin(string teamName);
        PagedResult<UserRequestDto> ShowRequests(PageQuery query);
        string HandleJoinRequest(bool answer, string username);
        string DeleteUserFromTeam(string username);
        PagedResult<UserDto> ShowUsers(PageQuery query);
    }
}