using ClinicApp.BLL.Services.Identity;
using ClinicApp.Core.Constant;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.DTO;
using ClinicApp.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    [Route("api/Account")]
    [ApiController]
    [TypeFilter(typeof(JwtAuthorizeAttribute))]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("v1/GetInfo")]
        
        public async Task<IActionResult> GetAccountInfo()
        {
            var _data = await userService.GetCurrentUser();
            return Ok(new CommonResponse() { Data = new { _data.Email, _data.UserName }, RequestStatus = RequestStatus.Success, Message = "Success" });
        }
    }
}
