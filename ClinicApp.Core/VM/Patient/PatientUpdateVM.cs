using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.VM.Patient
{
    public class PatientUpdateVM
    {
        public int? Id { get; set; }

        #region Basic

        public int PasNumber { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Forenames Required")]
        public string Forenames { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Surname Required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "DateOfBirth Required")]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "SexCode Required")]
        public string SexCode { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "HomeTelephoneNumber Required")]
        public string HomeTelephoneNumber { get; set; }
        #endregion

        #region NextOfKin
        [MaxLength(100)]
        [Required(ErrorMessage = "NokName Required")]
        public string NokName { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "NokRelationshipCode Required")]
        public string NokRelationshipCode { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "NokAddressLine1 Required")]
        public string NokAddressLine1 { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "NokAddressLine2 Required")]
        public string NokAddressLine2 { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "NokAddressLine3 Required")]
        public string NokAddressLine3 { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "NokAddressLine4 Required")]
        public string NokAddressLine4 { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "NokPostcode Required")]
        public string NokPostcode { get; set; }
        #endregion

        #region GpDetails
        //[MaxLength(100)]
        [Required(ErrorMessage = "GpCode Required")]
        public int GpCode { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "GpSurname Required")]
        public string GpSurname { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "GpInitials Required")]
        public string GpInitials { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "GpPhone Required")]
        public string GpPhone { get; set; }
        #endregion
    }
}
