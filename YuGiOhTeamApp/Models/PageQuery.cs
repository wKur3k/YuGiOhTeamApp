using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOhTeamApp.Models
{
    public class PageQuery
    {
        public string? SearchPhrase { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
    }
}
