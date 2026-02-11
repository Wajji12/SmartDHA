using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DHAFacilitationAPIs.Domain.Entities;

public class Property : BaseAuditableEntity
{
    public CategoryType? Category { get; set; }
    public PropertyType? Type { get; set; }
    public Phase? Phase { get; set; }
    public Zone? Zone { get; set; }
    public string Khayaban { get; set; } = string.Empty;
    public int Floor { get; set; }
    public string StreetNo { get; set; } = string.Empty;
    public int PlotNo { get; set; }
    public string Plot { get; set; } = string.Empty;
    public PossessionType PossessionType { get; set; }

    public string? ProofOfPossessionImage { get; set; }
    public string? UtilityBillAttachment { get; set; }
}
