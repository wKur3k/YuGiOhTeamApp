using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Entities;

namespace YuGiOhTeamApp
{
    public class DbSeeder
    {
       private readonly AppDbContext _dbContext;
        public DbSeeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Username = "admin",
                    PasswordHash = "AQAAAAEAACcQAAAAENpyEpnX0slp0M60tZIRRN9VhH6UwrYNCF/pzovKzxSa5kDi4Ta5H+ejhhGH3h5Txg==",
                    RoleId = 2
                }
            };
            return users;
        }
    }
}
