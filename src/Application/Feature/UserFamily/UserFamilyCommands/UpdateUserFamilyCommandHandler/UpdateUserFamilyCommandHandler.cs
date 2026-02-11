using MediatR;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.UpdateUserFamilyCommandHandler;

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

            entity.UserId = request.UserId;
            entity.Name = request.Name;
            entity.Relation = request.Relation;
            entity.Age = request.Age;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
