using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.Interfaces;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.DeleteUserFamilyCommandHandler;

public class DeleteUserFamilyCommandHandler
    : IRequestHandler<DeleteUserFamilyCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserFamilyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteUserFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserFamilies
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity == null)
            return false;

        _context.UserFamilies.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

