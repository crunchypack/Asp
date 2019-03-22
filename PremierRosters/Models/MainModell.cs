using PremierRosters.Areas.Identity.Pages.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class MainModell
    {
        public IEnumerable <TeamInfo> teamInfo { get; set; }
        public PlayerInfo playerInfo { get; set; }
     
    }
}
