using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.AddUserFamilyCommands;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.AddUserFamilyCommandHandler;

public class AddUserFamilyCommandHandler
    : IRequestHandler<AddUserFamilyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddUserFamilyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddUserFamilyCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserFamily
        {
            UserId = request.UserId,
            Name = request.Name,
            Relation = request.Relation,
            Age = request.Age
        };

        _context.UserFamilies.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
