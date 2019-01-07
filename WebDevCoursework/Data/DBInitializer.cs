using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDevCoursework.Models;

namespace WebDevCoursework.Data
{
    public static class DBInitializer
    {
        public static void Seed(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            EnsureContext(context);
            AddRolesAsync(roleManager).Wait();
            AddUsers(userManager);

        }

        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        private static void EnsureContext(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }

        private static void AddUsers(UserManager<User> userManager)
        {
            User manager = new User
            {
                UserName = "Member1@email.com",
                Email = "Member1@email.com",
                CustomMessage = "Hello"
            };

            User user1 = new User
            {
                UserName = "Customer1@email.com",
                Email = "Customer1@email.com",
                CustomMessage = "Hello!"
            };

            User user2 = new User
            {
                UserName = "Customer2@email.com",
                Email = "Customer2@email.com",
                CustomMessage = "Hello!"
            };

            User user3 = new User
            {
                UserName = "Customer3@email.com",
                Email = "Customer3@email.com",
                CustomMessage = "Hello!"
            };

            User user4 = new User
            {
                UserName = "Customer4@email.com",
                Email = "Customer4@email.com",
                CustomMessage = "Hello!"
            };

            User user5 = new User
            {
                UserName = "Customer5@email.com",
                Email = "Customer5@email.com",
                CustomMessage = "Hello!"
            };

            userManager.CreateAsync(manager, "Password123!").Wait();
            userManager.AddToRoleAsync(manager, "Admin").Wait();
            userManager.CreateAsync(user1, "Password123!").Wait();
            userManager.CreateAsync(user2, "Password123!").Wait();
            userManager.CreateAsync(user3, "Password123!").Wait();
            userManager.CreateAsync(user4, "Password123!").Wait();
            userManager.CreateAsync(user5, "Password123!").Wait();
        }


    }
}
