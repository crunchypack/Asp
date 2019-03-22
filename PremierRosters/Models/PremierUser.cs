using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class PremierUser : IdentityUser
    {
        public PremierUser() : base() { }
        public string Team { get; set; }
    }
}
