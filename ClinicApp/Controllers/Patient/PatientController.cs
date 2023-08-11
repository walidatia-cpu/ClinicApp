using ClinicApp.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers.Patient
{
    [Route("api/Patient")]
    [ApiController]
    [TypeFilter(typeof(JwtAuthorizeAttribute))]
    public class PatientController : ControllerBase
    {
        public PatientController()
        {

        }
    }
}
