using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface IAdminService
    {
        void DeleteUser(Guid userId);
        UserDto GetUserById(Guid id);
        PagedResult<UserDto> GetUsers(PageQuery query);
    }
}