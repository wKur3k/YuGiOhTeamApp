
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface IDecklistService
    {
        void CreateDecklist(IFormFile file);
        Tuple<byte[], string> DownloadDecklist(int id);
        void EditDecklist(int id, EditDecklistDto dto);
        string UploadDecklist(IFormFile file);
    }
}