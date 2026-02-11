using MediatR;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.UpdateUserFamilyCommandHandler;
using DHAFacilitationAPIs.Domain.Enums;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.UpdateUserFamilyCommandHandler
{
    public class UpdateUserFamilyCommandHandler
        : IRequestHandler<UpdateUserFamilyCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserFamilyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserFamilyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserFamilies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return false;

            entity.ApplicationUserId = request.UserId;
            entity.Name = request.Name;
            entity.Relation = (Relation)request.Relation;
            entity.DateOfBirth = request.DOB;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
