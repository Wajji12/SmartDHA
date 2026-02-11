using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.AddUserFamilyCommands;
using DHAFacilitationAPIs.Domain.Entities;
using DHAFacilitationAPIs.Domain.Enums;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.AddUserFamilyCommandHandler;

public class AddUserFamilyCommandHandler : IRequestHandler<AddUserFamilyCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public AddUserFamilyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddUserFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = new DHAFacilitationAPIs.Domain.Entities.UserFamily
        {
            ApplicationUserId = request.UserId,
            Name = request.Name,
            Relation = (Relation)request.Relation,
            DateOfBirth = request.DOB
        };

        await _context.UserFamilies.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
