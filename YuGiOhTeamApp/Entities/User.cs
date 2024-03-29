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
        public string PasswordHash { get; set; }
        public int RoleId { get; set; } = 1;
        public virtual Role Role { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public bool isLeader { get; set; }
        public List<UserRequests> UserRequests { get; set; }
        public List<Decklist> Decklists { get; set; }

    }
}
