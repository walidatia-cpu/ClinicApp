using ClinicApp.Core.Constant;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.DTO;
using ClinicApp.Core.Entities;
using ClinicApp.Core.JWT;
using ClinicApp.Core.VM.Identity;
using ClinicApp.Filters.ActionFilter;
using ClinicApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicApp.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("v1/login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            var Result = await userService.Login(model);
            if (Result.RequestStatus == RequestStatus.Unauthorized)
                return Unauthorized(Result);
            return Ok(Result);
        }


        


    }
}
