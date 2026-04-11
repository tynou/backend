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
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var response = await grpcClient.GetUserVerificationAsync(new UserVerificationRequest() { UserId = 1 });
        return Ok(response);
    }
    
    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> Test()
    {
        return Ok("Success");
    }
}