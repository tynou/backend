using MediatR;
using Notes.Application.Interfaces;
using Notes.Application.Models;

namespace Notes.Application.Features.Notes.GetNote;

public class GetNoteHandler(INoteRepository noteRepository) : IRequestHandler<GetNoteCommand, NoteDto>
{
    public async Task<NoteDto> Handle(GetNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await noteRepository.GetAsync(request.NoteId, request.UserId);
        if (note is null)
            throw new Exception("Note not found.");

        var noteDto = new NoteDto(
            note.Id,
            note.Title,
            note.Content,
            note.CreatedAt
        );

        return noteDto;
    }
}