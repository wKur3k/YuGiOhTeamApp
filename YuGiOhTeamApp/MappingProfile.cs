using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(m => m.TeamName, c => c.MapFrom(s => s.Team.Name));
            CreateMap<RegisterUserDto, User>();
            CreateMap<CreateTeamDto, Team>();
        }
    }
}
