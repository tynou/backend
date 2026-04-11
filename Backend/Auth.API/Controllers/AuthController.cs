using Auth.Application.Features.Auth.Login;
using Auth.Application.Features.Auth.Register;
using Auth.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

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
            registrationDto.Email
        );
        
        await mediator.Send(command);
        
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var command = new LoginCommand(loginDto.Username, loginDto.Password);
        var response = await mediator.Send(command);
        return Ok(response);
    }
    
    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> Test()
    {
        return Ok("Success");
    }
}