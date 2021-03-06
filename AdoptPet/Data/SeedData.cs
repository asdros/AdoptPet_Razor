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

            //context.Ad.AddRange(
            //    new Ad
            //    {
            //        Id = new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be45"),
            //        Title = "Śliczny kotek dupa",
            //        NormalizedLink = "sliczny-kotek-d62d1a",
            //        Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
            //        Sterilization = true,
            //        Deworming = true,
            //        AvailableFrom = DateTime.Now,
            //        LastModified = null,
            //        BreedId = 2,
            //        PlaceId = 1,
            //    },
            //    new Ad
            //    {
            //        Id = new Guid("25c3424d-79cd-40ca-a4e3-d71a14635145"),
            //        Title = "kot Bajtek",
            //        NormalizedLink = "kot-bajtek-s73hf",
            //        Description = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.",
            //        Sterilization = false,
            //        Deworming = true,
            //        AvailableFrom = DateTime.Now,
            //        LastModified = null,
            //        BreedId = 1,
            //        PlaceId = 3,
            //    },
            //    new Ad
            //    {
            //        Id = new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a5"),
            //        Title = "Azorek",
            //        NormalizedLink = "azorek-si83f",
            //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
            //        Sterilization = true,
            //        Deworming = true,
            //        AvailableFrom = DateTime.Now,
            //        LastModified = null,
            //        BreedId = 3,
            //        PlaceId = 3,
            //    });

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
