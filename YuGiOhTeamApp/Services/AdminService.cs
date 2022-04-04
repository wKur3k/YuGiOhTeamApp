using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public PagedResult<UserDto> GetUsers(PageQuery query)
        {
            var baseQuery = _context
                .Users
                .Include(u => u.Team)
                .Where(u => query.SearchPhrase == null ||
                (u.Username.ToLower().Contains(query.SearchPhrase.ToLower()) ||
                u.Team.Name.ToLower().Contains(query.SearchPhrase.ToLower())));
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { nameof(User.Username), r => r.Username },
                    { nameof(User.Team.Name), r => r.Team.Name }
                };
                var selectedColumns = columnsSelectors[query.SortBy];
                baseQuery = query.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumns) : baseQuery.OrderByDescending(selectedColumns);
            }
            var users = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();
            var totalItemsCount = baseQuery.Count();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            var result = new PagedResult<UserDto>(userDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public UserDto GetUserById(Guid id)
        {
            var user = _context
                .Users
                .Include(u => u.Team)
                .FirstOrDefault(u => u.Id == id);
            if (user is null)
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }
        public void DeleteUser(Guid userId)
        {
            var user = _context
                .Users
                .Include(u => u.Decklists)
                .Include(u => u.Team)
                .FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new BadHttpRequestException("User not found");
            }
            user.isLeader = false;
            var decklists = user.Decklists;
            var team = user.Team;
            _context.Decklists.RemoveRange(decklists);
            _context.Users.Where(u => u.TeamId == team.Id).ToList().Select(u => { u.TeamId = null; return u.TeamId; });
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }
    }
}
