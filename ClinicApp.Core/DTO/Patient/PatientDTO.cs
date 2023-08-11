using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.DTO.Patient
{
    public class PatientDTO
    {
       
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        #region Basic

        public int PasNumber { get; set; }
       
        public string Forenames { get; set; }
      
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
       
        public string SexCode { get; set; }
       
        public string HomeTelephoneNumber { get; set; }
        #endregion

        #region NextOfKin
        
        public string NokName { get; set; }
       
        public string NokRelationshipCode { get; set; }
       
        public string NokAddressLine1 { get; set; }
       
        public string NokAddressLine2 { get; set; }
       
        public string NokAddressLine3 { get; set; }
      
        public string NokAddressLine4 { get; set; }
       
        public string NokPostcode { get; set; }
        #endregion

        #region GpDetails
       
        public int GpCode { get; set; }
       
        public string GpSurname { get; set; }
        
        public string GpInitials { get; set; }
        
        public string GpPhone { get; set; }
        #endregion
    }
}
