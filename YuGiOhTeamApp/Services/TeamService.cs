using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public TeamService(AppDbContext context, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public void CreateTeam(CreateTeamDto dto)
        {
            var user = _context.Users.Include(u => u.Team).FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (user.Team is not null)
            {
                throw new BadHttpRequestException(" Cannot create/be in more than 1 team.");
            }
            var newTeam = _mapper.Map<Team>(dto);
            newTeam.LeaderId = user.Id;
            user.isLeader = true;
            user.Team = newTeam;
            _context.SaveChanges();
        }
        public bool ChangeDescription(string newDesc)
        {
            var user = _context.Users.Include(u => u.Team).FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (user.isLeader)
            {
                _context.Teams.FirstOrDefault(t => t.LeaderId == user.Id).Description = newDesc;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public void PassLeader(string username)
        {
            var user = _context.Users.Include(u => u.Team).FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            var newLeader = _context.Users.Include(u => u.Team).FirstOrDefault(u => u.Username == username);
            if (newLeader.TeamId != user.TeamId)
            {
                throw new BadHttpRequestException($"{username} is not in the same team as you.");
            }
            if (!user.isLeader)
            {
                throw new BadHttpRequestException("You are not team leader.");
            }
            if (newLeader is null)
            {
                throw new BadHttpRequestException($"New Leader username: {username},  is invalid.");
            }
            user.isLeader = false;
            newLeader.isLeader = true;
            user.Team.LeaderId = newLeader.Id;
            _context.SaveChanges();
        }
        public void DeleteTeam()
        {
            var user = _context.Users.Include(u => u.Team).FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (!user.isLeader)
            {
                throw new BadHttpRequestException("You are not team leader.");
            }
            user.isLeader = false;
            var team = _context.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == user.TeamId);
            if (team is null)
            {
                throw new BadHttpRequestException("Cannot get team.");
            }
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }
        public string RequestToJoin(string teamName)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (user.TeamId != null)
            {
                throw new BadHttpRequestException("You cannot be in team in order to send requests to join another team.");
            }
            if (string.IsNullOrEmpty(teamName))
            {
                throw new BadHttpRequestException("Team name can't be empty.");
            }
            if (_context.Teams.FirstOrDefault(t => t.Name == teamName) is null)
            {
                throw new BadHttpRequestException("Cannot find team with that name.");
            }
            var request = new UserRequests
            {
                UserId = user.Id,
                TeamId = _context.Teams.FirstOrDefault(t => t.Name == teamName).Id
            };
            _context.UserRequests.Add(request);
            _context.SaveChanges();
            return teamName;
        }
        public PagedResult<UserRequestDto> ShowRequests(PageQuery query)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (!user.isLeader)
            {
                throw new BadHttpRequestException("You are not team leader.");
            }
            var baseQuery = _context
                .UserRequests
                .Include(u => u.User)
                .Where(u => u.TeamId == user.TeamId)
                .Where(u => query.SearchPhrase == null ||
                u.User.Username.ToLower().Contains(query.SearchPhrase.ToLower()));
            baseQuery = query.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(r => r.User.Username) : baseQuery.OrderByDescending(r => r.User.Username);
            var users = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();
            var totalItemsCount = baseQuery.Count();
            var userDtos = _mapper.Map<List<UserRequestDto>>(users);
            var result = new PagedResult<UserRequestDto>(userDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }
        public string HandleJoinRequest (bool answer, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            var newUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (!user.isLeader)
            {
                throw new BadHttpRequestException("You are not team leader.");
            }
            if(newUser is null)
            {
                throw new BadHttpRequestException("Cannot find user with that username.");
            }
            if(_context.UserRequests.Where(u => u.TeamId == user.TeamId && u.UserId == newUser.Id) is null)
            {
                throw new BadHttpRequestException($"Cannot find {username} on request list to your team.");
            }
            if (answer)
            {
                _context.Teams.FirstOrDefault(t => t.Id == user.TeamId).Users.Add(newUser);
                var items = _context.UserRequests.Where(u => u.UserId == newUser.Id);
                _context.UserRequests.RemoveRange(items);
                _context.SaveChanges();
                return $"User: {username} joined your team.";
            }
            var item = _context.UserRequests.FirstOrDefault(u => u.TeamId == user.TeamId && u.UserId == newUser.Id);
            _context.Remove(item);
            _context.SaveChanges();
            return $"User: {username} got rejected.";
        }
        public string DeleteUserFromTeam(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            var removeUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (!user.isLeader)
            {
                throw new BadHttpRequestException("You are not team leader.");
            }
            if (removeUser is null || removeUser.TeamId != user.TeamId)
            {
                throw new BadHttpRequestException("Cannot find user with that username.");
            }
            _context.Users.FirstOrDefault(u => u.Username == username).TeamId = null;
            _context.SaveChanges();
            return $"Removed {username} from team";
        }
    }
}
