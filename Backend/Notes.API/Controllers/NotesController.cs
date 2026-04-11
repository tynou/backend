using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Models;
using Shared.Contracts.Grpc;

namespace Notes.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController(UserVerificationGrpc.UserVerificationGrpcClient grpcClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string title)
    {
        return Ok($"Note titled \"{title}\"");
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var userId = User.GetUserId();
        if (userId is null)
            return Unauthorized("Incorrect JWT token");
        
        var username = User.GetUsername();
        var response = await grpcClient.GetUserVerificationAsync(new UserVerificationRequest() { UserId = int.Parse(userId) });
        var test = new
        {
            id = userId,
            name = username,
            ver = response.IsVerified,
            exis = response.UserExists
        };
        return Ok(test);
    }
}