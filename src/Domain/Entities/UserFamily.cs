using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Domain.Entities;

public class UserFamily
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? Name { get; set; }
    public string? Relation { get; set; }
    public int Age { get; set; }

    public ApplicationUser? User { get; set; }
}
