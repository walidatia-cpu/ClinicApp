using ClinicApp.Core.DTO;
using ClinicApp.Core.VM.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Contracts.Patient
{
    public interface IPatientService
    {
        Task<CommonResponse> CreatePatientAsync(PatientVM patientVM);
        Task<CommonResponse> UpdatePatientAsync(PatientVM patientVM);
        Task<CommonResponse> UpdatePatientStatusAsync(int patientId);
        Task<CommonResponse> GetAllPatientAsync(int page);
        Task<CommonResponse> GetPatientByIdAsync(int patientId);
        Task<CommonResponse> RemovePatientByIdAsync(int patientId);
        
    }
}
