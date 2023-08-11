using ClinicApp.Core.Constant;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.DTO;
using ClinicApp.Core.Entities;
using ClinicApp.Core.JWT;
using ClinicApp.Core.VM.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.BLL.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoleService _roleService;
       
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IOptions<JWTSettings> options;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IRoleService roleRepository,  RoleManager<ApplicationRole> roleManager, IOptions<JWTSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleService = roleRepository;
            _roleManager = roleManager;
            this.options = options;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task CreateUserAsync(string UserName, string password, string roleName)
        {
            var user = new ApplicationUser
            {
                UserName = UserName,
                Email = UserName
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
            if (!await UserExistsAsync("Owner"))
            {
                await CreateUserAsync("Owner", "Owner@123", Role.SuperAdmin.ToString());
            }
            if (!await UserExistsAsync("Doctor"))
            {
                await CreateUserAsync("Doctor", "Doctor@123", Role.Doctor.ToString());
            }
            if (!await UserExistsAsync("Patient"))
            {
                await CreateUserAsync("Patient", "Patient@123", Role.Patient.ToString());
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var _jwtSettings = options.Value;

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<CommonResponse> Login(LoginVM model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                var _data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                };
                return new CommonResponse { Message = "Success", RequestStatus = RequestStatus.Success,Data= _data };
            }
            return new CommonResponse { Message = "Unauthorized", RequestStatus = RequestStatus.Unauthorized };
        
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
           string userid = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
           return await _userManager.FindByIdAsync(userid);
        }
    }
}
