using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Exceptions;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Common.Models;
using DHAFacilitationAPIs.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DHAFacilitationAPIs.Application.Feature.Auth.Commands.UserSignup;
public class UserSignupCommand : IRequest<SignupVM>
{
    public string Cnic { get; set; } = "";
    public string CellNo { get; set; } = "";
}
public class UserSignupCommandHandler : IRequestHandler<UserSignupCommand, SignupVM>
{
    private readonly IProcedureService _sp;
    public UserSignupCommandHandler(IProcedureService sp)
    {
        _sp = sp;
    }
    public async Task<SignupVM> Handle(UserSignupCommand request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();
        p.Add("@CNICNO", request.Cnic, DbType.String, size: 150);
        p.Add("@CellNo", request.CellNo, DbType.String, size: 15);
        p.Add("@msg", dbType: DbType.String, size: 150, direction: ParameterDirection.Output);
        p.Add("@OTP", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@OutCellNo", dbType: DbType.String, size: 15, direction: ParameterDirection.Output);

        // 2) execute
        await _sp.ExecuteAsync(
            "USP_ApplyForRegistration",
            p,
            cancellationToken
        );

        return new SignupVM
        {
            OutCellNo = p.Get<string>("@OutCellNo") ?? request.CellNo,
            Msg = p.Get<string>("@msg") ?? "No message",
            Otp= p.Get<int>("@OTP").ToString()
        };
    }
}
