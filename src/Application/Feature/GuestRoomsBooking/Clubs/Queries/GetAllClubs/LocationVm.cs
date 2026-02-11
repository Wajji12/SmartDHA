using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Queries.GetAllClubs;
public class LocationVm
{
    public int LocationId { get; set; }
    public string Location { get; set; } = "";
    public string FullName { get; set; } = "";
    public string LocationType { get; set; } = "";
    public int LocationTypeID { get; set; }
}
