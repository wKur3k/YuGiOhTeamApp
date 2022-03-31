using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Models
{
    public class Deck
    {
        public List<string> Main { get; set; }
        public List<string> Extra { get; set; }
        public List<string> Side { get; set; }

        public Deck(List<string> main, List<string> extra, List<string> side)
        {
            Main = main;
            Extra = extra;
            Side = side;
        }
    }
}
