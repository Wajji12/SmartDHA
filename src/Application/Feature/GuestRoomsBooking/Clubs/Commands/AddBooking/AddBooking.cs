using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Commands.AddBooking;
public class AddBookingCommand : IRequest<AddBookingVM>
{
    public int UserId { get; set; }
    public int BookingForUserId { get; set; }
    public string VenueLocationIds { get; set; } = "";
    public string VenueCategoryIds { get; set; } = "";
    public string OccupancyTypeIds { get; set; } = "";
    public int NumberOfVenues { get; set; }
    public DateTime DatesFrom { get; set; }
    public DateTime DatesTo { get; set; }
}
public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, AddBookingVM>
{
    private readonly IProcedureService _sp;

    public AddBookingCommandHandler(IProcedureService sp)
    {
        _sp = sp;
    }

    public async Task<AddBookingVM> Handle(AddBookingCommand request, CancellationToken cancellationToken)
    {
        var p = new DynamicParameters();

        p.Add("@UserID", request.UserId);
        p.Add("@BookingForUserID", request.BookingForUserId);
        p.Add("@VenueLocationIDs", request.VenueLocationIds);
        p.Add("@VenueCategoryIDs", request.VenueCategoryIds);
        p.Add("@OccupencyTypeIDs", request.OccupancyTypeIds);
        p.Add("@NumberOfVenues", request.NumberOfVenues);
        p.Add("@Datesfrom", request.DatesFrom);
        p.Add("@DatesTo", request.DatesTo);
        p.Add("@MSG", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);
        p.Add("@PaymentID", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

        var (outParams, rows) = await _sp.ExecuteWithListAsync<dynamic>("USP_addVenueBooking",p,cancellationToken);

        var responseData = rows?
        .Where(row => row != null)
        .SelectMany(row =>
        {
             if (row is IDictionary<string, object> dict)
             {
                 return dict.Select(kvp => new BookingDataVm
                 {
                     Column = kvp.Key,
                     Value = kvp.Value
                 });
             }
             else
             {
                 return Enumerable.Empty<BookingDataVm>();
             }
        })
        .ToList() ?? new List<BookingDataVm>();


        return new AddBookingVM
        {
            Msg = outParams.Get<string>("@MSG") ?? "",
            BillNo = outParams.Get<string>("@PaymentID") ?? "",
            ResponseData = responseData
        };
    }
}
