using Microsoft.AspNetCore.Identity;

namespace DHAFacilitationAPIs.Domain.Entities;
public class ApplicationUser : BaseAuditableEntity
{
    public string FullName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string Password { get; set; } = default!;
    public CategoryType Category { get; set; } = default!;
    public PropertyType Property { get; set; } = default!;
    public ResidenceStatus ResidenceStatus { get; set; } = default!;
    public Phase Phase { get; set; } = default!;
    public string LaneNumber { get; set; } = default!;
    public string PlotNumber { get; set; } = default!;
    public string Floor { get; set; } = default!;
    public string? FrontSideCNIC { get; set; }
    public string? BackSideCNIC { get; set; }
    public string CNIC { get; set; } = default!;


    public ICollection<UserFamily> UserFamilies { get; set; } = new List<UserFamily>();
}
