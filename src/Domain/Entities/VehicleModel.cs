using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Domain.Entities;

public class VehicleModel
{
    public int Id { get; set; }
    public int VehicleMakeId { get; set; }
    public string? Name { get; set; }

}
