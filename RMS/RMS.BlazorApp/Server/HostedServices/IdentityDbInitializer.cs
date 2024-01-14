using Microsoft.AspNetCore.Identity;

namespace RMS.BlazorApp.HostedServices 
{
    public class IdentityDbInitializer
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public IdentityDbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }   
        public async Task SeedAsync()
        {
            string[] roles = { "Admin", "Staff", "Operator" };
            foreach (string role in roles)
            {
                await this.CreateRole(role);
            }
            var user1 = new IdentityUser { UserName = "Admin" };
            await CreateUser(user1, "@Open1234", "Admin");
            var user2 = new IdentityUser { UserName = "R54" };
            await CreateUser(user2, "@Open1234", "Staff");
        }
        private async Task CreateRole(string roleName)
        {
            var exists = await roleManager.RoleExistsAsync(roleName);
            if(!exists)
            {
               await  roleManager.CreateAsync(new IdentityRole {  Name = roleName, NormalizedName=roleName.ToUpper() });
            }
        }
        private async Task CreateUser(IdentityUser user, string password, string role)
        {
            var u = await userManager.FindByNameAsync(user.UserName ?? "");
            if(u == null)
            {
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
