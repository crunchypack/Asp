using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class Filter
    {
        public IEnumerable<TeamInfo> teamInfo { get; set; }
        public IEnumerable <PlayerInfo> playerInfo{ get; set; }
        public IEnumerable<SponsorInfo> sponsors { get; set; }
        public PlayerSponsorBy SpPl { get; set; }
    }
}
