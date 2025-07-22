using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Queries.SearchRoom;
public class SearchRoomVm
{
    public string Msg { get; set; } = "";
    public List<SearchRoomVms> Rooms { get; set; } = new();

   
}

public class SearchRoomVms
{
    public string Location { get; set; } = "";
    public string Category { get; set; } = "";
    public int Rant { get; set; }
    public int Noofrooms { get; set; }
}
