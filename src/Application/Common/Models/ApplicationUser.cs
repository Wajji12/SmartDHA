using Microsoft.AspNetCore.Identity;

namespace DHAFacilitationAPIs.Application.Common.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = default!;
}
