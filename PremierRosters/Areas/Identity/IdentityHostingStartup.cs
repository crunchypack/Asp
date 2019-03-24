using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PremierRosters.Models;

[assembly: HostingStartup(typeof(PremierRosters.Areas.Identity.IdentityHostingStartup))]
namespace PremierRosters.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PremierRostersContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PremierRostersContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>()
                //   .AddEntityFrameworkStores<PremierRostersContext>();

                services.AddIdentity<PremierUser,PremierRole>()
                    .AddEntityFrameworkStores<PremierRostersContext>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI();

               

            });
        }
    }
}