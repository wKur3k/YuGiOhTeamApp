using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Entities
{
    public class UserRequests
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

    }
}
