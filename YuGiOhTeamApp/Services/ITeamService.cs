using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface ITeamService
    {
        bool ChangeDescription(string newDesc);
        void CreateTeam(CreateTeamDto dto);
        void passLeader(string username);
        void deleteTeam();
        string requestToJoin(string teamName);
        List<UserRequestDto> showRequests(PageQuery query);
    }
}