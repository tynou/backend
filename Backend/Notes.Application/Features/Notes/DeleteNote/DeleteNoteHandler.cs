using MediatR;
using Notes.Application.Interfaces;

namespace Notes.Application.Features.Notes.DeleteNote;

public class DeleteNoteHandler(INoteRepository noteRepository) : IRequestHandler<DeleteNodeCommand>
{
    public async Task Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
    {
        var success = await noteRepository.DeleteAsync(request.NoteId, request.UserId);

        if (!success)
            throw new Exception("Could not delete the note.");
    }
}