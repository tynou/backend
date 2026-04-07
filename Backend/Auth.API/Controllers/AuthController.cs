using Auth.Application.Features.Auth.Register;
using Auth.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        var command = new RegisterCommand(
            registrationDto.Username, 
            registrationDto.Password,
            registrationDto.PhoneNumber
        );
        
        var result = await mediator.Send(command);
        
        return Ok(new { Message = result });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        return Ok("123");
    }
}