using ClinicApp.Core.Contracts.Patient;
using ClinicApp.Core.DTO;
using ClinicApp.Core.VM.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.BLL.Services.Patient
{
    public class PatientService : IPatientService
    {   
        public Task<CommonResponse> CreatePatientAsync(PatientVM patientVM)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> GetAllPatientAsync(int page)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> GetPatientByIdAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> RemovePatientByIdAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> UpdatePatientAsync(PatientVM patientVM)
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> UpdatePatientStatusAsync(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
