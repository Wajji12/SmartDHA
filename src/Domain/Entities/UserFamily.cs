using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Domain.Entities;
public class UserFamily : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? RfidTag { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Cnic { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FatherName { get; set; }
    public Relation Relation { get; set; }
    public DateTime DateOfBirth { get; set; }
}
