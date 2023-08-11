using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Entities
{
    public class Doctor : BaseEntity
    {
        public Doctor()
        {
            Patients = new List<Patient>();
        }
        [ForeignKey("Account")]
        public string AccountId { get; set; }
        public virtual ApplicationUser Account { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
