using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LupExercise_ArchieGonzales.DAL
{
    public class Seed
    {
        public static async Task InitializeRoleAync(RoleManager<IdentityRole> roleManager)
        {

            foreach (string role in ConfigSettings.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
        }
    }
}
