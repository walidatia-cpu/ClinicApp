using AutoMapper;
using Azure;
using ClinicApp.Core.Constant;
using ClinicApp.Core.Contracts;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.Contracts.Patients;
using ClinicApp.Core.DTO;
using ClinicApp.Core.DTO.Patient;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.BLL.Services.Patients
{
    public class PatientService : IPatientService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAsyncRepository<Patient> patientRepository;
        private readonly IUserService userService;
        private readonly IAsyncRepository<Doctor> doctorRepository;

        public PatientService(IMapper mapper, IUnitOfWork unitOfWork, IAsyncRepository<Patient> patientRepository, IUserService userService, IAsyncRepository<Doctor> doctorRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.patientRepository = patientRepository;
            this.userService = userService;
            this.doctorRepository = doctorRepository;
        }
        public async Task<CommonResponse> CreatePatientAsync(PatientVM patientVM)
        {
            try
            {

                var obj = mapper.Map<Patient>(patientVM);
                obj.IsActive = true;
                obj.CreationDate = DateTime.Now;
                obj.ModificationDate = DateTime.Now;
                obj.DoctorId = (int)patientVM.DoctorId;
                obj.AccountId = patientVM.AccountId;
                await patientRepository.AddAsync(obj);
                await unitOfWork.SaveChangesAsync();
                return new CommonResponse { RequestStatus = RequestStatus.Success, Message = "Addedsuccessfully" };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse> GetAllPatientAsync(int page, int count)
        {
            try
            {
                var TotalCount = patientRepository.GetTotalCountAsync();
                var _list = await patientRepository.GetAllAsync(page, count);
                var Patients = mapper.Map<List<PatientDTO>>(_list);
                var _data = new { TotalCount, Patients };
                return new CommonResponse { RequestStatus = RequestStatus.Success, Message = "Success", Data = _data };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse> GetPatientByIdAsync(int patientId)
        {
            try
            {
                var _Patient = await patientRepository.FirstOrDefaultAsync(c => c.Id == patientId);
                if (_Patient == null)
                    return new CommonResponse { RequestStatus = RequestStatus.NotFound, Message = "NotFound" };

                var _data = mapper.Map<PatientDTO>(_Patient);
                return new CommonResponse { RequestStatus = RequestStatus.Success, Message = "Success", Data = _data };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse> RemovePatientByIdAsync(int patientId)
        {
            try
            {
                var _Patient = await patientRepository.FirstOrDefaultAsync(c => c.Id == patientId);
                if (_Patient == null)
                    return new CommonResponse { RequestStatus = RequestStatus.NotFound, Message = "NotFound" };

                await patientRepository.RemoveAsync(_Patient);
                await unitOfWork.SaveChangesAsync();

                return new CommonResponse { RequestStatus = RequestStatus.Success, Message = "Success" };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse> UpdatePatientAsync(PatientUpdateVM patientVM)
        {
            try
            {
                var _Patient = await patientRepository.FirstOrDefaultAsync(c => c.Id == patientVM.Id);
                if (_Patient == null)
                    return new CommonResponse { RequestStatus = RequestStatus.NotFound, Message = "NotFound" };
                _Patient.DateOfBirth = patientVM.DateOfBirth;
                _Patient.PasNumber = patientVM.PasNumber;
                _Patient.Forenames = patientVM.Forenames;
                _Patient.Surname = patientVM.Surname;
                _Patient.SexCode = patientVM.SexCode;
                _Patient.HomeTelephoneNumber = patientVM.HomeTelephoneNumber;
                _Patient.NokName = patientVM.NokName;
                _Patient.NokRelationshipCode = patientVM.NokRelationshipCode;
                _Patient.NokAddressLine1 = patientVM.NokAddressLine1;
                _Patient.NokAddressLine2 = patientVM.NokAddressLine2;
                _Patient.NokAddressLine3 = patientVM.NokAddressLine3;
                _Patient.NokAddressLine4 = patientVM.NokAddressLine4;
                _Patient.NokPostcode = patientVM.NokPostcode;
                _Patient.GpCode = patientVM.GpCode;
                _Patient.GpSurname = patientVM.GpSurname;
                _Patient.GpInitials = patientVM.GpInitials;
                _Patient.GpPhone = patientVM.GpPhone;
                _Patient.ModificationDate = DateTime.Now;
                await patientRepository.UpdateAsync(_Patient);
                await unitOfWork.SaveChangesAsync();
                return new CommonResponse { RequestStatus = RequestStatus.Success, Message = "Updatedsuccessfully" };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<CommonResponse> UpdatePatientStatusAsync(int patientId)
        {
            try
            {
                var _Patient = await patientRepository.FirstOrDefaultAsync(c => c.Id == patientId);
                if (_Patient == null)
                    return new CommonResponse { RequestStatus = RequestStatus.NotFound, Message = "NotFound" };
                _Patient.IsActive = !_Patient.IsActive;
                await patientRepository.UpdateAsync(_Patient);
                await unitOfWork.SaveChangesAsync();

                return new CommonResponse { RequestStatus = RequestStatus.Success, Message = "Success" };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }
    }
}
