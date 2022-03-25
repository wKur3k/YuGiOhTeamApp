﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int PasswordHash { get; set; }
        public string Avatar { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

    }
}