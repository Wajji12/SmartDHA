using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.Auth.Queries.ResendOTP;
public class ResendOtpVm
{
    public string Cnic { get; set; } = "";
    public string Msg { get; set; } = "";
    public int Otp { get; set; }
    public string MobileNo { get; set; } = "";
}
