using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class PlayerInfo
    {
        public PlayerInfo()
        {
        }
        public string FirstName { set; get; }
        public string Surname { set; get; }
        public int Jersey { set; get; }
        public string Position { set; get; }
        public int BirthYear { set; get; }
        public int Team { set; get; }
        public string  TeamString { set; get; }
        public int ID { set; get; }
    }
}
