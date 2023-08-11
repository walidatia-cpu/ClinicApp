using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.VM.Patient
{
    public class PatientVM
    {
        #region Basic

        public int PasNumber { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string Forenames { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string SexCode { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string HomeTelephoneNumber { get; set; }
        #endregion

        #region NextOfKin
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string NokName { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string NokRelationshipCode { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Required")]
        public string NokAddressLine1 { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Required")]
        public string NokAddressLine2 { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string NokAddressLine3 { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Required")]
        public string NokAddressLine4 { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string NokPostcode { get; set; }
        #endregion

        #region GpDetails
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public int GpCode { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string GpSurname { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string GpInitials { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Required")]
        public string GpPhone { get; set; }
        #endregion
    }
}
