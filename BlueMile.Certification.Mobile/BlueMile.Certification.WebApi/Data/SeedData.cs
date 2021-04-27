using BlueMile.Certification.Data.Static;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager).ConfigureAwait(false);
            await SeedUsersAsync(userManager).ConfigureAwait(false);
        }

        private static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@bluemile.co.za") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@bluemile.co.za"
                };

                var result = await userManager.CreateAsync(user, "Password@1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRoles), UserRoles.Administrator)).ConfigureAwait(false);
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(Enum.GetName(typeof(UserRoles), role)))
                {
                    var newRole = new IdentityRole
                    {
                        Name = Enum.GetName(typeof(UserRoles), role)
                    };
                    await roleManager.CreateAsync(newRole);
                }
            }
        }
    }
}
