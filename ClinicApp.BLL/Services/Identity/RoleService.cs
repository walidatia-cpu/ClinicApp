using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.BLL.Services.Identity
{
    public class RoleService: IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task CreateRoleAsync(string roleName)
        {
            await _roleManager.CreateAsync(new ApplicationRole { Name= roleName });
        }
        public async Task CreateDefaultRolesAsync()
        {
            if (!await RoleExistsAsync("Admin"))
                await CreateRoleAsync("Admin");

            if (!await RoleExistsAsync("User"))
                await CreateRoleAsync("User");
        }
    }
}
