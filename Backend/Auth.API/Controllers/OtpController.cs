using Auth.Application.Features.Otp.SendCode;
using Auth.Application.Features.Otp.Verify;
using Auth.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController(IMediator mediator) : ControllerBase
{
    [HttpPost("send-code")]
    public async Task<IActionResult> SendCode([FromBody] SendOtpDto sendOtpDto)
    {
        var command = new SendOtpCommand(sendOtpDto.Type, sendOtpDto.Identifier);
        await mediator.Send(command);
        return Ok();
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] VerifyOtpDto verifyOtpDto)
    {
        var command = new VerifyOtpCommand(verifyOtpDto.Identifier, verifyOtpDto.Code);
        var result = await mediator.Send(command);
        return Ok(new { Result = result });
    }
}