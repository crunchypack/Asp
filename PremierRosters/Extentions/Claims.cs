using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PremierRosters.Extentions
{
    public static class Claims
    {
        public static string GetTeam(this ClaimsPrincipal principal)
        {
            var Team = principal.Claims.FirstOrDefault(c => c.Type == "Team");
            return Team?.Value;
        }
    }
}
