using System.Security.Claims;
using System.Threading;
using Azure.Core;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.Feature.Auth.Commands.SetPassword;
using DHAFacilitationAPIs.Application.Feature.Auth.Commands.UserSignup;
using DHAFacilitationAPIs.Application.Feature.Auth.Commands.ValidateOTP;
using DHAFacilitationAPIs.Application.Feature.Auth.Queries.ResendOTP;
using DHAFacilitationAPIs.Application.Feature.Auth.Queries.ValidateUser;
using DHAFacilitationAPIs.Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobileAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly ISmsService _smsService;
    private readonly IAuthenticationService _authenticationService;

    public AuthController(ISmsService smsService, IAuthenticationService authenticationService)
    {
        _smsService = smsService;
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(ValidateUserQuery request)
    {
        var user = await Mediator.Send(request);

        bool isSuccessful = false;
        if(user.Otp>0)
        {
            isSuccessful = true;
        }

        var result = new
        {
            cnic = user.Cnic,
            msg = user.Msg,
            otp = user.Otp,
            mobileNo = user.MobileNo,
            isSuccessfullyLogin = isSuccessful
        };

        if (isSuccessful)
        {
            
                return Ok(result);
                //var message = $"Your code is {user.Otp}.";
                //var smsResponse = await _smsService.SendSmsAsync(user.MobileNo, message);

                //if (!smsResponse.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
                //    return Ok(result);

                //return BadRequest(new { cnic = request.Cnic, msg = "Error sending OTP. Please contact support." });
        }

        return Unauthorized(result);
    }

    [AllowAnonymous]
    [HttpPost("SignUp")]
    public async Task<IActionResult> Signup(UserSignupCommand request)
    {
        // 1) create user in DB / call your SP etc.
        var user = await Mediator.Send(request);

        // 2) send OTP only if >0
        if (int.TryParse(user.Otp, out var otp) && otp > 0)
        {
            var message = $"Your code is {otp}.";
            var smsResponse = await _smsService.SendSmsAsync(user.OutCellNo, message);

            if (!smsResponse.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
                return Ok(new { cnic = request.Cnic, msg = user.Msg });

            return BadRequest(new { cnic = request.Cnic, msg = "Error sending OTP. Please contact support." });
        }
        return NotFound(new { cnic = request.Cnic, msg = user.Msg });
    }

    [AllowAnonymous]
    [HttpPost("ValidateOTP")]
    public async Task<IActionResult> ValidateOTP(ValidateOTPCommand request)
    {
        // 1) create user in DB / call your SP etc.
        var user = await Mediator.Send(request);


        var successMessages = new[]
        {
            "You have successfully registered on the DHA Portal.",
            "Login successfully."
        };

        bool isSuccessful = successMessages.Any(msg =>
            user.Msg.Contains(msg, StringComparison.OrdinalIgnoreCase));

        if (isSuccessful)
        {
            var customClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Cnic),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
             };

            var token = await _authenticationService.GenerateTokenAsync(customClaims);

            var result = new
            {
                cnic = user.Cnic,
                msg = user.Msg,
                isSuccessfullyLogin = true,
                token = token
            };

            return Ok(result);
        }
        else
        {
            var result = new
            {
                cnic = user.Cnic,
                msg = user.Msg,
                isSuccessfullyLogin = false
            };

            return Unauthorized(result);
        }


    }

    
    [HttpPost("SetPassword")]
    public async Task<IActionResult> SetPassword(SetPasswordCommand request)
    {
        return Ok(await Mediator.Send(request));
    }

    [HttpPost("ResendOTP")]
    public async Task<IActionResult> ResendOTP(ResendOTPQuery request)
    {
        var result = await Mediator.Send(request);

        if (result.Otp > 0)
        {
            var message = $"Your code is {result.Otp}.";
            var smsResponse = await _smsService.SendSmsAsync(result.MobileNo, message);

            if (!smsResponse.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
                return Ok(result);

            return BadRequest(new { cnic = request.Cnic, msg = "Error sending OTP. Please contact support." });
        }
        else
        {
            return BadRequest(new
            {
                cnic = result.Cnic,
                msg = result.Msg,
                isSuccessfullyResent = false
            });
        }
    }
}
