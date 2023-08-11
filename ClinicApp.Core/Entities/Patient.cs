using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Entities
{
    public class Patient: BaseEntity
    {
        #region Basic
       
        public int PasNumber { get; set; }
        [MaxLength(100)]
        public string Forenames { get; set; }
        [MaxLength(100)]
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(100)]
        public string SexCode { get; set; }
        [MaxLength(100)]
        public string HomeTelephoneNumber { get; set; }
        #endregion

        #region NextOfKin
        [MaxLength(100)]
        public string NokName { get; set; }
        [MaxLength(100)]
        public string NokRelationshipCode { get; set; }
        [MaxLength(200)]
        public string NokAddressLine1 { get; set; }
        [MaxLength(200)]
        public string NokAddressLine2 { get; set; }
        [MaxLength(100)]
        public string NokAddressLine3 { get; set; }
        [MaxLength(200)]
        public string NokAddressLine4 { get; set; }
        [MaxLength(100)]
        public string NokPostcode { get; set; }
        #endregion

        #region GpDetails
        [MaxLength(100)]
        public int GpCode { get; set; }
        [MaxLength(100)]
        public string GpSurname { get; set; }
        [MaxLength(100)]
        public string GpInitials { get; set; }
        [MaxLength(100)]
        public string GpPhone { get; set; }
        #endregion

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Account")]
        public string AccountId { get; set; }
        public virtual ApplicationUser Account { get; set; }
    }
}
