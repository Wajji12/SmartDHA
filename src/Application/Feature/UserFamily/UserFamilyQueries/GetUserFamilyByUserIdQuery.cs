using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyQueries;

public class GetUserFamilyByUserIdQuery : IRequest<List<UserFamily>>
{
    public int UserId { get; set; }
}
