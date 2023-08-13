using ClinicApp.BLL.Services.Identity;
using ClinicApp.BLL.Services.Patients;
using ClinicApp.Core.Constant;
using ClinicApp.Core.Contracts.Doctors;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.Contracts.Patients;
using ClinicApp.Core.DTO;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Identity;
using ClinicApp.Core.VM.Patient;
using ClinicApp.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Controllers.Patient
{
    [Route("api/Patient")]
    [ApiController]
    [TypeFilter(typeof(JwtAuthorizeAttribute))]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IUserService userService;
        private readonly IDoctorService doctorService;

        public PatientController(IPatientService patientService, IUserService userService, IDoctorService doctorService)
        {
            this.patientService = patientService;
            this.userService = userService;
            this.doctorService = doctorService;
        }

        [HttpPost]
        [Route("v1/Add")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientVM model)
        {
            Random rnd = new Random();
            int number = rnd.Next(1000, 9999);
            var currentuser = await userService.GetCurrentUser();
            if(!await userService.CkeckUserInRole(currentuser,Role.Doctor.ToString()))
                return Ok(new CommonResponse { RequestStatus=RequestStatus.AccessDenied, Message= "AccessDenied" });
            var _Result = await userService.CreateUserAsync(model.PasNumber.ToString()+"@"+number.ToString(), model.PasNumber.ToString() + "@" + number.ToString(), Role.Patient.ToString());
            if (_Result.RequestStatus != RequestStatus.Success)
                return Ok(_Result);
            var _doctor =await doctorService.GetDoctorByAccountIdAsync(currentuser.Id);
            model.DoctorId = _doctor.Id;
            model.AccountId = _Result.Data.ToString();
            var Result = await patientService.CreatePatientAsync(model);
            return Ok(Result);
        }

        [HttpPost]
        [Route("v1/Update")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientUpdateVM model)
        {
            var currentuser = await userService.GetCurrentUser();
            if (!await userService.CkeckUserInRole(currentuser, Role.Doctor.ToString()))
                return Ok(new CommonResponse { RequestStatus = RequestStatus.AccessDenied, Message = "AccessDenied" });
            var _doctor = await doctorService.GetDoctorByAccountIdAsync(currentuser.Id);
            var Result = await patientService.UpdatePatientByDoctorAsync(model, _doctor.Id);
            return Ok(Result);
        }

        [HttpPost]
        [Route("v1/GetAll/page/{page}/count/{count}")]
        public async Task<IActionResult> GetAllPatient([FromRoute] int page, [FromRoute] int count)
        {
            var currentuser = await userService.GetCurrentUser();
            if (!await userService.CkeckUserInRole(currentuser, Role.Doctor.ToString()))
                return Ok(new CommonResponse { RequestStatus = RequestStatus.AccessDenied, Message = "AccessDenied" });
            var _doctor = await doctorService.GetDoctorByAccountIdAsync(currentuser.Id);
            var Result = await patientService.GetAllPatientByDoctorIdAsync(page, count, _doctor.Id);
            return Ok(Result);
        }
        [HttpPost]
        [Route("v1/GetById/{Id}")]
        public async Task<IActionResult> GetPatientById([FromRoute] int Id)
        {
            var currentuser = await userService.GetCurrentUser();
            if (!await userService.CkeckUserInRole(currentuser, Role.Doctor.ToString()))
                return Ok(new CommonResponse { RequestStatus = RequestStatus.AccessDenied, Message = "AccessDenied" });
            var _doctor = await doctorService.GetDoctorByAccountIdAsync(currentuser.Id);
            var Result = await patientService.GetPatientByIdByDoctorAsync(Id,_doctor.Id);
            return Ok(Result);
        }
        [HttpPost]
        [Route("v1/UpdateStatusById/{Id}")]
        public async Task<IActionResult> UpdatePatientStatus([FromRoute] int Id)
        {
            var currentuser = await userService.GetCurrentUser();
            if (!await userService.CkeckUserInRole(currentuser, Role.Doctor.ToString()))
                return Ok(new CommonResponse { RequestStatus = RequestStatus.AccessDenied, Message = "AccessDenied" });
            var _doctor = await doctorService.GetDoctorByAccountIdAsync(currentuser.Id);
            var Result = await patientService.UpdatePatientStatusByDoctorAsync(Id, _doctor.Id);
            return Ok(Result);
        }

        [HttpPost]
        [Route("v1/RemovePatientById/{Id}")]
        public async Task<IActionResult> RemovePatientById([FromRoute] int Id)
        {
            var currentuser = await userService.GetCurrentUser();
            if (!await userService.CkeckUserInRole(currentuser, Role.Doctor.ToString()))
                return Ok(new CommonResponse { RequestStatus = RequestStatus.AccessDenied, Message = "AccessDenied" });
            var _doctor = await doctorService.GetDoctorByAccountIdAsync(currentuser.Id);
            var Result = await patientService.RemovePatientByIdByDoctorAsync(Id, _doctor.Id);
            return Ok(Result);
        }
       

    }
}
