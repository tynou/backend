using MediatR;
using Notes.Application.Interfaces;

namespace Notes.Application.Features.Notes.UpdateNote;

public class UpdateNoteHandler(INoteRepository noteRepository) : IRequestHandler<UpdateNoteCommand>
{
    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var success = await noteRepository.UpdateAsync(request.NoteId, request.UserId, request.Title, request.Content);

        if (!success)
            throw new Exception("Note could not be updated.");
    }
}