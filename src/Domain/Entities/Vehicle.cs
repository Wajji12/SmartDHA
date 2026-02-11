using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Domain.Entities;
public class Vehicle : BaseAuditableEntity
{
    public int LicenseNo { get; set; } 
    public string License { get; set; } = null!;
    public string? Attachment { get; set; } = null!;
    public string Year { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
}
