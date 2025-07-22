using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.Auth.Commands.UserSignup;
using DHAFacilitationAPIs.Application.ViewModels;

namespace DHAFacilitationAPIs.Application.Feature.Auth.Commands.ValidateOTP;
public class ValidateOTPCommand : IRequest<ValidateOtpVm>
{
    public int Otp { get; set; }
    public string Cnic { get; set; } = "";
}
public class ValidateOTPCommandHandler : IRequestHandler<ValidateOTPCommand, ValidateOtpVm>
{
    private readonly IProcedureService _sp;
    public ValidateOTPCommandHandler(IProcedureService sp)
    {
        _sp = sp;
    }
    public async Task<ValidateOtpVm> Handle(ValidateOTPCommand request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();
        p.Add("@OTP", request.Otp, DbType.Int32);
        p.Add("@CNICNO", request.Cnic, DbType.String, size: 150);
        p.Add("@msg", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);

        var (pOut, userRow) = await _sp.ExecuteWithSingleRowAsync<dynamic>(
            "USP_ValidateOTP",
            p,
            cancellationToken
        );

        return new ValidateOtpVm
        {
            Msg = pOut.Get<string>("@msg") ?? "",
            FullName = userRow?.Name ?? "",
            Cnic = userRow?.CNIC ?? "",
            MobileNo = userRow?.MobileNo ?? "",
            Email = userRow?.email ?? "",
            RegNo = userRow?.RegistrationNo ?? "",
            MemNo = userRow?.membershipNo ?? "",
            Role = userRow?.Role ?? ""
        };
    }
}

