using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Commands.AddBooking;
public class AddBookingVM
{
    public string Msg { get; set; } = "";
    public string BillNo { get; set; } = "";
    public List<BookingDataVm> ResponseData { get; set; } = new();
}

public class BookingDataVm
{
    public string Column { get; set; } = "";
    public object? Value { get; set; }
}
