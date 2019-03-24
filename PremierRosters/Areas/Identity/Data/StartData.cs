using Microsoft.AspNetCore.Identity;
using PremierRosters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Areas.Identity.Data
{
    public class StartData
    {
        public static async Task Initialize(PremierRostersContext context,
                             UserManager<PremierUser> userManager,
                             RoleManager<PremierRole> roleManager)
        {
            // Databasen existerar
            context.Database.EnsureCreated();

            // Deklarering av variabler
            String adminId1 = "";
            String adminId2 = "";

            string role1 = "Admin";
            string desc1 = "This is the administrator role";

            string role2 = "Coach";
            string desc2 = "This is the headcoach role";

            string password = "Qwert9=";
            // Skapa roller om de inte finns 
            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new PremierRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new PremierRole(role2, desc2, DateTime.Now));
            }
            // Skapa användare om den inte finns
            if (await userManager.FindByNameAsync("Admin@admin.se") == null)
            {
                var user = new PremierUser
                {
                    UserName = "Admin@admin.se",
                    Email = "Admin@admin.se",
                    PhoneNumber = "6902341234",
                    Team = "All"

                };
                // Tilldela lösenord och roll
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("pep@mancity.com") == null)
            {
                var user = new PremierUser
                {
                    UserName = "pep@mancity.com",
                    Email = "pep@mancity.com",
                    PhoneNumber = "7788951456",
                    Team = "Manchester City"
                    
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
                adminId2 = user.Id;       
            }
        }
    }
}
