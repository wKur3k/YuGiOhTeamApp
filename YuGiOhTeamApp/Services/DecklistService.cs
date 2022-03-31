using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Services
{
    public class DecklistService : IDecklistService
    {
        private readonly IUserContextService _userContextService;

        public DecklistService(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }
        public string UploadDecklist(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var username = _userContextService.User.Identity.Name.ToString();
                var fileName = file.FileName;
                if (!fileName.EndsWith(".ydk"))
                {
                    throw new BadHttpRequestException("Decklist must be .ydk file.");
                }
                var rootPath = Directory.GetCurrentDirectory();
                var fullPath = $"{rootPath}/UserFiles/{username}/Decklists/{fileName}";
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return "Decklist Uploaded";
            }
            throw new BadHttpRequestException("Something went wrong");
        }
    }
}
