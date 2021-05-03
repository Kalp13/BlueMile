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
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                var roleName = Enum.GetName<UserRoles>((UserRoles)role);
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var newRole = new IdentityRole
                    {
                        Name = roleName
                    };
                    await roleManager.CreateAsync(newRole);
                }
            }
        }
    }
}
