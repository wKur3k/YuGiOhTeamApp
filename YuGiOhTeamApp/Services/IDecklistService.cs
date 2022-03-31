
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface IDecklistService
    {
        void CreateDecklist(IFormFile file);
        string UploadDecklist(IFormFile file);
    }
}