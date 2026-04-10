using Auth.Application.Features.Otp.SendCode;
using Auth.Application.Features.Otp.Verify;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OtpController(IMediator mediator) : ControllerBase
{
    [HttpPost("send-code")]
    public async Task<IActionResult> SendCode()
    {
        await mediator.Send(new SendOtpCommand("1rczhvwds@gmail.com"));
        return Ok();
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify()
    {
        var result = await mediator.Send(new VerifyOtpCommand("1rczhvwds@gmail.com", "736767"));
        return Ok(new { Result = result });
    }
}