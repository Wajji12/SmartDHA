using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyQueries
{
    public class GetAllUserFamilyQueryHandler : IRequestHandler<GetAllUserFamilyQuery, List<UserFamily>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUserFamilyQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserFamily>> Handle(GetAllUserFamilyQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserFamilies
                .Include(x => x.User) // optional: include related ApplicationUser
                .ToListAsync(cancellationToken);
        }
    }
}
