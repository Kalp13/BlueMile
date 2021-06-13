using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlueMile.Certification.WASM.Server.Data
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
                    await userManager.AddToRoleAsync(user, "Administrator").ConfigureAwait(false);
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Owner"))
            {
                var role = new IdentityRole
                {
                    Name = "Owner"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
