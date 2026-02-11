using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.Auth.Queries.ResendOTP;
using MediatR;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Queries.GetAllClubs;
public class GetAllClubsQuery : IRequest<List<LocationVm>>
{
    public int UserId { get; set; }
}
public class GetAllClubsQueryHandler : IRequestHandler<GetAllClubsQuery, List<LocationVm>>
{
    private readonly IProcedureService _sp;

    public GetAllClubsQueryHandler(IProcedureService sp)
    {
        _sp = sp;
    }

    public async Task<List<LocationVm>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();
        p.Add("@user_id", request.UserId);

        var (outParams, rows) = await _sp.ExecuteWithListAsync<LocationVm>(
            "USP_SelectLocation",
            p,
            cancellationToken
        );

        return rows;
    }
}


