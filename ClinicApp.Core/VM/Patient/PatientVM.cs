using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.VM.Patient
{
    public class PatientVM: PatientUpdateVM
    {
        public string? AccountId { get; set; }
        public int? DoctorId { get; set; }
        
    }
}
