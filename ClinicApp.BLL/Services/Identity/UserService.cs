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
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;

        public UserService(UserManager<ApplicationUser> userManager, IRoleService roleRepository)
        {
            _userManager = userManager;
            _roleService = roleRepository;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task CreateUserAsync(string email, string password, string roleName)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await _roleService.RoleExistsAsync(roleName))
                    await _roleService.CreateRoleAsync(roleName);

                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
        public async Task CreateDefaultUsersAsync()
        {
            if (!await UserExistsAsync("admin@example.com"))
            {
                await CreateUserAsync("admin@example.com", "YourPassword123", "Admin");
            }
        }
    }
}
