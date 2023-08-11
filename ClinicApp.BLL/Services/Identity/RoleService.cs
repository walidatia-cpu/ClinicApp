using ClinicApp.Core.Constant;
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
            if (!await RoleExistsAsync(Role.SuperAdmin.ToString()))
                await CreateRoleAsync(Role.SuperAdmin.ToString());

            if (!await RoleExistsAsync(Role.Doctor.ToString()))
                await CreateRoleAsync(Role.Doctor.ToString());

            if (!await RoleExistsAsync(Role.Patient.ToString()))
                await CreateRoleAsync(Role.Patient.ToString());
        }
    }
}
