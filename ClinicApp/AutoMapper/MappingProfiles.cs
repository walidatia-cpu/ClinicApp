using AutoMapper;
using ClinicApp.Core.DTO.Doctors;
using ClinicApp.Core.DTO.Patient;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Doctors;
using ClinicApp.Core.VM.Patient;

namespace ClinicApp.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           
            #region Patient
            _MapPatient();
            #endregion

            #region Doctor
            _MapDoctor();
            #endregion
        }

        #region Patient
        void _MapPatient()
        {
             CreateMap<Patient, PatientDTO>();
             CreateMap<PatientVM, Patient>();
             CreateMap<PatientUpdateVM, Patient>();
        }
        #endregion

        #region Doctor
        void _MapDoctor()
        {
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<DoctorVM, Doctor>();
        }
        #endregion
    }
}
