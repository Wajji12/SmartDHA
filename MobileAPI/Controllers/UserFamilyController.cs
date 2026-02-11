using DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Commands.AddBooking;
using DHAFacilitationAPIs.Application.Feature.UserFamily.Commands.AddUserFamilyCommandHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserFamilyController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("add-user-family")]
    public async Task<IActionResult> AddUserFamily(AddUserFamilyCommand request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }
}
