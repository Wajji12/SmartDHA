using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.ApplicationUser;

public class LoginRequest
{
    public string CNIC { get; set; } = null!;
    public string Password { get; set; } = null!;
}

