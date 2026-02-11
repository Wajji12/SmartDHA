using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading;
using Azure.Core;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.Auth.Queries.ResendOTP;
using DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Commands.AddBooking;
using DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Commands.CancelBooking;
using DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Queries.GetAllClubs;
using DHAFacilitationAPIs.Application.Feature.GuestRoomsBooking.Clubs.Queries.SearchRoom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobileAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GuestRoomsBookingController : BaseApiController
{
    private readonly ISmsService _smsService;
    public GuestRoomsBookingController(ISmsService smsService)
    {
        _smsService = smsService;
    }

    [HttpGet("GetAllClubs/{user_id}")]
    public async Task<IActionResult> GetAllClubsAsync(int user_id, CancellationToken cancellationToken)
    {
        var query = new GetAllClubsQuery { UserId = user_id };
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("SearchRoom")]
    public async Task<IActionResult> SearchRoom(SearchRoomQuery query,CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);

        if (result.Rooms.Any())
            return Ok(result);

        return NotFound(new { msg = result.Msg, rooms = result.Rooms });

    }

    [HttpPost("AddBooking")]
    public async Task<IActionResult> AddBooking(AddBookingCommand request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }


    [HttpPost]
    [Route("CancelBooking")]
    public async Task<IActionResult> CancelBooking(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(request, cancellationToken));
    }

}
