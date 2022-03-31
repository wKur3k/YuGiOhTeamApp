using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public class DecklistService : IDecklistService
    {
        private readonly IUserContextService _userContextService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DecklistService(IUserContextService userContextService, AppDbContext context, IMapper mapper)
        {
            _userContextService = userContextService;
            _context = context;
            _mapper = mapper;
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
                return fullPath;
            }
            throw new BadHttpRequestException("Something went wrong");
        }
        public void CreateDecklist(IFormFile file)
        {
            var newDecklist = new Decklist()
            {
                Path = UploadDecklist(file),
                UserId = (Guid)_userContextService.GetUserId,
                Name = file.FileName,
                Visibility = Visibility.PRIVATE
            };
            _context.Decklists.Add(newDecklist);
            _context.SaveChanges();
        }
        public void EditDecklist(int id, EditDecklistDto dto)
        {
            var decklist = _context.Decklists.FirstOrDefault(d => d.Id == id);
            if(decklist.UserId != _userContextService.GetUserId)
            {
                throw new BadHttpRequestException("Wrong decklist id.");
            }
            if (String.IsNullOrEmpty(dto.Name))
            {
                decklist.Visibility = dto.Visibility;
            }
            else
            {
                decklist.Visibility = dto.Visibility;
                decklist.Name = dto.Name;
            }
            _context.SaveChanges();
        }
    }
}
