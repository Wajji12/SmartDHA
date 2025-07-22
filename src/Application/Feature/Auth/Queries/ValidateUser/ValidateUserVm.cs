using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.Auth.Queries.ValidateUser;
public class ValidateUserVm
{
    public string Cnic { get; set; } = "";
    public string Msg { get; set; } = "";
    public int Otp { get; set; }
    public string MobileNo { get; set; } = "";
}

