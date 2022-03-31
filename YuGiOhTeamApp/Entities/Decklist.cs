using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuGiOhTeamApp.Models;

namespace YuGiOhTeamApp.Entities
{
    public class Decklist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string path { get; set; }
        public Visibility visibility { get; set; }
        public Deck Deck { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
