using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.Auth.Commands.SetPassword;

namespace DHAFacilitationAPIs.Application.Feature.Auth.Queries.ValidateUser;
public class ValidateUserQuery : IRequest<ValidateUserVm>
{
    public string Cnic { get; set; } = "";
    public string Password { get; set; } = "";
}
public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, ValidateUserVm>
{
    private readonly IProcedureService _sp;

    public ValidateUserQueryHandler(IProcedureService sp)
    {
        _sp = sp;
    }

    public async Task<ValidateUserVm> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();

        p.Add("@CNICNO", request.Cnic);
        p.Add("@Password", request.Password);
        p.Add("@msg", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);
        p.Add("@OTP", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@cellNo", dbType: DbType.String, size: 15, direction: ParameterDirection.Output);

        await _sp.ExecuteAsync(
            "USP_ValidateUser",
            p,
            cancellationToken
        );

        return new ValidateUserVm
        {
            Cnic = request.Cnic,
            Msg = p.Get<string>("@msg") ?? "",
            Otp = p.Get<int>("@OTP"),
            MobileNo = p.Get<string>("@cellNo") ?? ""
        };
    }
}
