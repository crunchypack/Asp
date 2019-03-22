using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class PremierRole : IdentityRole
    {
        public PremierRole() :base() { }
        public PremierRole(string roleName): base(roleName)
        {

        }
        public PremierRole(string roleName, string desc, DateTime createDate) : base(roleName)
        {
            this.Description = desc;
            this.CreateDate = createDate;
        }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
