using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface ITeamService
    {
        bool ChangeDescription(string newDesc);
        void CreateTeam(CreateTeamDto dto);
        void passLeader(string username);
        void deleteAdmin(int teamId);
    }
}