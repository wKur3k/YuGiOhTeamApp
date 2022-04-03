
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public interface IDecklistService
    {
        void CreateDecklist(IFormFile file);
        Tuple<byte[], string> DownloadDecklist(int id);
        void EditDecklist(int id, EditDecklistDto dto);
        PagedResult<DecklistDto> getAll(PageQuery query, Visibility visibility);
        string GetDecklistDetails(int id);
        string TranslateFile(string path);
        string UploadDecklist(IFormFile file);
    }
}