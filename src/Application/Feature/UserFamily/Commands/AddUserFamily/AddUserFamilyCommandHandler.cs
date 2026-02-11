using System.Threading;
using System.Threading.Tasks;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.UserFamily.Commands.AddUserFamilyCommandHandler;
using DHAFacilitationAPIs.Domain.Entities;
using DHAFacilitationAPIs.Domain.Enums;
using MediatR;

namespace DHAFacilitationAPIs.Application.Feature.UserFamily.UserFamilyCommands.AddUserFamilyCommandHandler;

public class AddUserFamilyCommandHandler : IRequestHandler<AddUserFamilyCommand, AddUserFamilyResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public AddUserFamilyCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<AddUserFamilyResponse> Handle(AddUserFamilyCommand request, CancellationToken cancellationToken)
    {
        var response = new AddUserFamilyResponse();
        
        var entity = new DHAFacilitationAPIs.Domain.Entities.UserFamily
        {
            Name = request.Name,
            Relation = (Relation)request.Relation,
            DateOfBirth = request.DOB,
            Cnic = request.CNIC,
            FatherHusbandName = request.FatherHusbandName,
            ProfilePicture = request.ProfilePicture,
            PhoneNumber = request.PhoneNo,
            ApplicationUserId = Guid.Parse(request.ApplicationUserId)
        };

        await _context.UserFamilies.AddAsync(entity);
        try
        {

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        response.Success = true;
        response.Message = "Family member added successfully.";
        response.Id = entity.Id;
        return response;
    }
}
