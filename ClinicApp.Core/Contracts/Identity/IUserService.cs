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
        Task CreateUserAsync(string email, string password, string roleName);
        Task CreateDefaultUsersAsync();
    }
}
