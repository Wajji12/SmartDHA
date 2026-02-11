using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.Interfaces;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyQueries;

public class GetUserFamilyByIdQueryHandler
    : IRequestHandler<GetUserFamilyByIdQuery, UserFamily>
{
    private readonly ApplicationDbContext _context;

    public GetUserFamilyByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserFamily> Handle(GetUserFamilyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.UserFamilies
            .FirstOrDefaultAsync(x => x.Id == request.Id);
    }
}

