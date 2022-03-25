using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwtToken(LoginUserDto dto);
        PagedResult<UserDto> GetUsers(PageQuery query);
    }
}