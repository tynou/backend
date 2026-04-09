using Auth.Application.Features.Otp.SendCode;
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
        await mediator.Send(new SendCodeCommand("1rczhvwds@gmail.com"));
        return Ok();
    }
    
    [HttpPost("verify")]
    public async Task<IActionResult> Verify()
    {
        return Ok();
    }
}