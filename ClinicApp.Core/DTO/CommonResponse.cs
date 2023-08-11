using ClinicApp.Core.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.DTO
{
    public class CommonResponse
    {
        public RequestStatus RequestStatus { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object ModelError { get; set; }
       
    }
}
