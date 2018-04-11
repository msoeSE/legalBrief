using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BriefAssistant.Data;
using Microsoft.AspNetCore.Identity;

namespace BriefAssistant.Services
{
    public static class RoleInitializer
    {
        public static async Task Initialize(RoleManager<ApplicationRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Lawyer"))
            {
                var role = new ApplicationRole("Lawyer");
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                var role = new ApplicationRole("User");
                await roleManager.CreateAsync(role);
            }
        }
    }
}
