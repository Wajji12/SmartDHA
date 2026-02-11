using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DHAFacilitationAPIs.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public string? Category { get; set; }
    public string? Type { get; set; }
    public string? Phase { get; set; }
    public string? Zone { get; set; }
    public string? Khayaban { get; set; }
    public string? Floor { get; set; }
    public string? StreetType { get; set; }
    public string? StreetNO { get; set; }
    public string? PlotNO { get; set; }
    public PossessionType PossessionType { get; set; }

    public string? ProofOfPossessionImage { get; set; }
    public string? UtilityBillAttachment { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedOnUTC { get; set; } = DateTime.UtcNow;
}
