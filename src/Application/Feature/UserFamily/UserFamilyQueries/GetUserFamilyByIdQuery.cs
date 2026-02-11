using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyQueries;

public class GetUserFamilyByIdQuery : IRequest<UserFamily>
{
    public int Id { get; set; }
}
