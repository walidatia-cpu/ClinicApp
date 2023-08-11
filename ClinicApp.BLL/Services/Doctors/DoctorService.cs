using AutoMapper;
using ClinicApp.Core.Constant;
using ClinicApp.Core.Contracts;
using ClinicApp.Core.Contracts.Doctors;
using ClinicApp.Core.Contracts.Identity;
using ClinicApp.Core.DTO;
using ClinicApp.Core.Entities;
using ClinicApp.Core.VM.Doctors;

namespace ClinicApp.BLL.Services.Doctors
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAsyncRepository<Doctor> doctorRepository;
       

        public DoctorService(IMapper mapper, IUnitOfWork unitOfWork, IAsyncRepository<Doctor> doctorRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.doctorRepository = doctorRepository;
           
        }
        public async Task<CommonResponse> CreateDoctorAsync(DoctorVM DoctorVM)
        {
            try
            {
                
                var obj = mapper.Map<Doctor>(DoctorVM);
                obj.IsActive = true;
                obj.CreationDate = DateTime.Now;
                obj.ModificationDate = DateTime.Now;
                obj.AccountId = DoctorVM.AccountId;
                await doctorRepository.AddAsync(obj);
                await unitOfWork.SaveChangesAsync();
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "Addedsuccessfully" };
            }
            catch (Exception ex)
            {
                return new CommonResponse { RequestStatus = RequestStatus.ServerError, Message = "ServerError" };
            }
        }

        public async Task<Doctor> GetDoctorByAccountIdAsync(string AccountId)
        {
          return await doctorRepository.FirstOrDefaultAsync(c => c.AccountId == AccountId);
        }
    }
}
