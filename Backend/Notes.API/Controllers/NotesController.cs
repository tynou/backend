using Common.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Features.Notes.CreateNote;
using Notes.Application.Features.Notes.DeleteNote;
using Notes.Application.Features.Notes.GetNote;
using Notes.Application.Features.Notes.UpdateNote;
using Notes.Application.Models;

namespace Notes.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetNote(int id)
    {
        var command = new GetNoteCommand(id, User.GetUserId());

        var note = await mediator.Send(command);
        
        return Ok(note);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto createNoteDto)
    {
        var command = new CreateNoteCommand(
            User.GetUserId(),
            createNoteDto.Title,
            createNoteDto.Content,
            createNoteDto.CreatedAt
        );

        await mediator.Send(command);

        return Created();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = new UpdateNoteCommand(id, User.GetUserId(), updateNoteDto.Title, updateNoteDto.Content);

        await mediator.Send(command);
        
        return Ok("Note successfully updated.");
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var command = new DeleteNodeCommand(id, User.GetUserId());
        
        await mediator.Send(command);

        return Ok("Note successfully deleted.");
    }
}