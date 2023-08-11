using AutoMapper;
using ClinicApp.Core.DTO.Patient;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Patient;

namespace ClinicApp.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<YourEntity, YourDto>();
            #region Patient
            _MapPatient();
            #endregion
        }
        #region Patient
        void _MapPatient()
        {
             CreateMap<Patient, PatientDTO>();
             CreateMap<PatientVM, Patient>();
        }
        #endregion
    }
}
