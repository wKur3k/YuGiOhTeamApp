
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface IDecklistService
    {
        void CreateDecklist(IFormFile file);
        void EditDecklist(int id, EditDecklistDto dto);
        string UploadDecklist(IFormFile file);
    }
}