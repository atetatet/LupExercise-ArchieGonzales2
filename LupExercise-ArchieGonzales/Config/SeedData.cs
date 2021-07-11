using LupExercise_ArchieGonzales.DAL;
using LupExercise_ArchieGonzales.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LupExercise_ArchieGonzales.Config
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            await InitializeIdentityAync(serviceProvider);
        }

        public static async Task InitializeIdentityAync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await LupExercise_ArchieGonzales.DAL.Seed.InitializeRoleAync(roleManager);
            var userManager = serviceProvider.GetService<UserManager<Users>>();
            var adminUser = await userManager.GetUsersInRoleAsync(ConfigSettings.AdminRole);
            if (adminUser.Count == 0)
            {
                var user = new Users
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "XS?wMc6P19");

                await userManager.AddToRoleAsync(user, ConfigSettings.AdminRole);
            }
        }
    }
}
