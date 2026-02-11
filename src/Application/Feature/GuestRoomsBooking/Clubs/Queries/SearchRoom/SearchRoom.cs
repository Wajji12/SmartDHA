using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Queries.SearchRoom;
public class SearchRoomQuery : IRequest<SearchRoomVm>
{
    public int UserId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int LocationId { get; set; }
    public string BookFor { get; set; } = "";
}
public class SearchRoomQueryHandler : IRequestHandler<SearchRoomQuery, SearchRoomVm>
{
    private readonly IProcedureService _sp;

    public SearchRoomQueryHandler(IProcedureService sp)
    {
        _sp = sp;
    }

    public async Task<SearchRoomVm> Handle(SearchRoomQuery request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();

        p.Add("@UserID", request.UserId);
        p.Add("@DateFrom", request.FromDate);
        p.Add("@dateTo", request.ToDate);
        p.Add("@LocationID", request.LocationId);
        p.Add("@BookFor", request.BookFor);
        p.Add("@msg", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);


        var (outParams, rows) = await _sp.ExecuteWithListAsync<SearchRoomVms>(
    "USP_SelectVenueAvailableForBooking",
    p,
    cancellationToken
);


        var result = new SearchRoomVm
        {
            Msg = rows.Any()
                ? "Rooms fetched successfully"
                : "No rooms available",
            Rooms = rows
        };

        return result;

    }
}
