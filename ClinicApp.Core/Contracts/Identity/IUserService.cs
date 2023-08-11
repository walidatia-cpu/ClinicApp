using ClinicApp.Core.DTO;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Contracts.Identity
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(string email);
        Task<CommonResponse> CreateUserAsync(string email, string password, string roleName);
        Task CreateDefaultUsersAsync();
        Task<CommonResponse> Login(LoginVM model);
        Task<ApplicationUser> GetCurrentUser();
        Task<bool> CkeckUserInRole(ApplicationUser user, string role);
    }
}
