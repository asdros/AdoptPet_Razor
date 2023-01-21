using AdoptPet.Areas.Authorization;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptPet.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string entryUserPassword)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminID = await EnsureUser(serviceProvider, entryUserPassword, "admin@email.com");
                await EnsureRole(serviceProvider, adminID, Constants.AdministratorsRole);

                var managerID = await EnsureUser(serviceProvider, entryUserPassword, "manager@email.com");
                await EnsureRole(serviceProvider, managerID, Constants.ManagersRole);

                SeedDB(context, adminID);
            }
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            context.SaveChanges();
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string entryUserPassword, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName
                };

                await userManager.CreateAsync(user, entryUserPassword);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough.");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult identityResult;

            if (!await roleManager.RoleExistsAsync(role))
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough.");
            }

            identityResult = await userManager.AddToRoleAsync(user, role);

            return identityResult;
        }

    }
}
