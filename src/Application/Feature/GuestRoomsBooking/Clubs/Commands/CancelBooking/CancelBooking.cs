using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Commands.CancelBooking;
using System.Data;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using MediatR;

public class CancelBookingCommand : IRequest<CancelBookingResponseVm>
{
    public int UserId { get; set; }
    public int BookingMasterId { get; set; }
    public int BookingDetailId { get; set; }
    public string CancelationRemarks { get; set; } = "";
}
public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, CancelBookingResponseVm>
{
    private readonly IProcedureService _sp;

    public CancelBookingCommandHandler(IProcedureService sp)
    {
        _sp = sp;
    }

    public async Task<CancelBookingResponseVm> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();

        p.Add("@User_id", request.UserId);
        p.Add("@VenueBookingMasterID", request.BookingMasterId);
        p.Add("@VenueBookingDetail_ID", request.BookingDetailId);
        p.Add("@Remarks", request.CancelationRemarks);
        p.Add("@MSG", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

        await _sp.ExecuteAsync("USP_CancelVenueBooking", p, cancellationToken);

        return new CancelBookingResponseVm
        {
            Msg = p.Get<string>("@MSG") ?? ""
        };
    }
}
