using ClinicApp.BLL.Services.Identity;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ClinicApp.Security
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public IOptions<JWTSettings> JwtSettings { get; set; }
        public IUserService UserService { get; }

        public JwtAuthorizeAttribute(IOptions<JWTSettings> jwtSettings, IUserService userService)
        {
            JwtSettings = jwtSettings;
            UserService = userService;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
           var _jwtSettings = JwtSettings.Value;
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                context.HttpContext.User = principal;
                var user = UserService.GetCurrentUser().Result;
                if (user == null)
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                    return;
                }
            }
            catch (Exception)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }
        }
    }
}
