using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Services
{

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSetting;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSetting, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSetting = authenticationSetting;
            _mapper = mapper;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public string GenerateJwtToken(LoginUserDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == dto.Username);
            if(user is null)
            {
                throw new BadHttpRequestException("Invalid Username or Password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadHttpRequestException("Invalid Username or Password");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            if (user.TeamId != null)
            {
                claims.Add(new Claim("TeamId", user.TeamId.ToString()));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(_authenticationSetting.JwtExpireHours);
            var token = new JwtSecurityToken(_authenticationSetting.JwtIssuer,
                _authenticationSetting.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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
    }
}
