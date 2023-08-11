using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Core.Constant
{
    public enum RequestStatus
    {
        Success=200,ServerError=500,Unauthorized=401, BadRequest=400, AccessDenied=403,NotFound=404
    }
}
