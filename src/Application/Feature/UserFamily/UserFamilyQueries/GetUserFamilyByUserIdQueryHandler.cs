using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.Interfaces;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyQueries;

public class GetUserFamilyByUserIdQueryHandler
    : IRequestHandler<GetUserFamilyByUserIdQuery, List<UserFamily>>
{
    private readonly IApplicationDbContext _context;

    public GetUserFamilyByUserIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserFamily>> Handle(GetUserFamilyByUserIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.UserFamilies
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return data;
    }
}
