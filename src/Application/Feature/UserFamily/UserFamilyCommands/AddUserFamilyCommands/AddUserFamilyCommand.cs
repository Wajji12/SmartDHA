using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.AddUserFamilyCommands;

public class AddUserFamilyCommand : IRequest<int>
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Relation { get; set; }
    public int Age { get; set; }
}
