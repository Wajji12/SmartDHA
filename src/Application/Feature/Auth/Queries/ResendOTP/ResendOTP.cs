using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.Auth.Queries.ValidateUser;

namespace DHAFacilitationAPIs.Application.Feature.Auth.Queries.ResendOTP;
public class ResendOTPQuery : IRequest<ResendOtpVm>
{
    public string Cnic { get; set; } = "";
}
public class ResendOTPQueryHandler : IRequestHandler<ResendOTPQuery, ResendOtpVm>
{
    private readonly IProcedureService _sp;

    public ResendOTPQueryHandler(IProcedureService sp)
    {
        _sp = sp;
    }

    public async Task<ResendOtpVm> Handle(ResendOTPQuery request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();

        p.Add("@CNICNO", request.Cnic);
        p.Add("@msg", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);
        p.Add("@OTP", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@CELLNO", dbType: DbType.String, size: 15, direction: ParameterDirection.Output);

        await _sp.ExecuteAsync(
            "USP_RegenerateOTP",
            p,
            cancellationToken
        );

        return new ResendOtpVm
        {
            Cnic = request.Cnic,
            Msg = p.Get<string>("@msg") ?? "",
            Otp = p.Get<int>("@OTP"),
            MobileNo = p.Get<string>("@CELLNO") ?? ""
        };
    }
}

