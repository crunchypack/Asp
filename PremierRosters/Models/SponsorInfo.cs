using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class SponsorInfo
    {
        public SponsorInfo() { }
        public string Name { set; get; }
        public int ID { set; get; }
        public List<KeyValuePair<string,int>> Players { set; get; }
    }
}
