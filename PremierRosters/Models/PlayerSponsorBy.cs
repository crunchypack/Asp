using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class PlayerSponsorBy
    {
        public int Sponsor { set; get; }
        public int Player { set; get; }
        public List<KeyValuePair<int,int>> SponsoredBy { get;set; }
    }
}
