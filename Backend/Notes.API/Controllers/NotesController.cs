using System.Net;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces;
using Notes.Application.Models;
using Notes.Domain.Entities;
using Shared.Contracts.Grpc;

namespace Notes.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController(UserVerificationGrpc.UserVerificationGrpcClient grpcClient, INoteRepository noteRepository) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string title)
    {
        var userId = User.GetUserId();
        if (userId is null)
            return Unauthorized("Incorrect JWT token.");
        
        var note = await noteRepository.GetByTitleAsync(int.Parse(userId), title);
        if (note is null)
            return NotFound($"Note titled \"{title}\" not found.");
        
        var noteDto = new NoteDto(
            note.Id,
            note.Title,
            note.Content,
            note.CreatedAt
        );
        return Ok(noteDto);
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var userId = User.GetUserId();
        if (userId is null)
            return Unauthorized("Incorrect JWT token.");
        
        var response = await grpcClient.GetUserVerificationAsync(new UserVerificationRequest() { UserId = int.Parse(userId) });
        if (!response.UserExists || !response.IsVerified)
            return Unauthorized("User does not exist or is not verified.");

        var note = new Note()
        {
            UserId = int.Parse(userId),
            Title = createNoteDto.Title,
            Content = createNoteDto.Content,
            CreatedAt = createNoteDto.CreatedAt ?? DateTime.UtcNow
        };
        
        await noteRepository.CreateAsync(note);
        
        return Created();
    }
}