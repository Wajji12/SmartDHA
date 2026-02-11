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

namespace DHAFacilitationAPIs.Application.Feature.Auth.Commands.SetPassword;
public class SetPasswordCommand : IRequest<SetPasswordVm>
{
    public string Cnic { get; set; } = "";
    public string Password { get; set; } = "";
    public string OldPassword { get; set; } = "";
}
public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, SetPasswordVm>
{
    private readonly IProcedureService _sp;
    public SetPasswordCommandHandler(IProcedureService sp)
    {
        _sp = sp;
    }
    public async Task<SetPasswordVm> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();
        p.Add("@CNICNO", request.Cnic);
        p.Add("@password", request.Password);
        p.Add("@OldPassword", request.OldPassword);
        p.Add("@msg", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);

        await _sp.ExecuteAsync(
            "USP_SetPassword",
            p,
            cancellationToken
        );

        return new SetPasswordVm
        {
            Cnic = request.Cnic,
            Msg = p.Get<string>("@msg") ?? ""
        };
    }
}

