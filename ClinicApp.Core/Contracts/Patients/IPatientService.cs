using ClinicApp.Core.DTO;
using ClinicApp.Core.VM.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Contracts.Patients
{
    public interface IPatientService
    {
        Task<CommonResponse> CreatePatientAsync(PatientVM patientVM);
        Task<CommonResponse> UpdatePatientByDoctorAsync(PatientUpdateVM patientVM, int doctorId);
        Task<CommonResponse> UpdatePatientStatusByDoctorAsync(int patientId, int doctorId);
        Task<CommonResponse> GetAllPatientAsync(int page,int count);
        Task<CommonResponse> GetAllPatientByDoctorIdAsync(int page,int count,int doctorId);
        Task<CommonResponse> GetPatientByIdByDoctorAsync(int patientId, int doctorId);
        Task<CommonResponse> RemovePatientByIdByDoctorAsync(int patientId, int doctorId);
        
    }
}
