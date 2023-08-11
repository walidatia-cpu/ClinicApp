using ClinicApp.Core.DTO;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Doctors;
using ClinicApp.Core.VM.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Contracts.Doctors
{
    public interface IDoctorService
    {
        Task<CommonResponse> CreateDoctorAsync(DoctorVM  doctorVM);
        Task<Doctor> GetDoctorByAccountIdAsync(string AccountId);
    }
}
