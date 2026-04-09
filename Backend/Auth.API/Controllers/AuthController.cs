using Auth.Application.Features.Auth.Login;
using Auth.Application.Features.Auth.Register;
using Auth.Application.Models;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IMediator mediator, IPublishEndpoint publishEndpoint) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        await publishEndpoint.Publish(new SendExampleEvent("User is trying to register..."));
        
        var command = new RegisterCommand(
            registrationDto.Username, 
            registrationDto.Password,
            registrationDto.PhoneNumber
        );
        
        var response = await mediator.Send(command);
        
        return Ok(new { Message = response });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var command = new LoginCommand(loginDto.Username, loginDto.Password);
        var response = await mediator.Send(command);
        return Ok(response);
    }
}