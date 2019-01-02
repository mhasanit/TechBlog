using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blog.Data;
using Microsoft.AspNetCore.Identity;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {

                var scop = host.Services.CreateScope();

                var ctx = scop.ServiceProvider.GetRequiredService<AppDbcontext>();
                var userMgr = scop.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = scop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("admin");


                if (!ctx.Roles.Any())
                {

                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@mhd.com"
                    };

                    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

                host.Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
