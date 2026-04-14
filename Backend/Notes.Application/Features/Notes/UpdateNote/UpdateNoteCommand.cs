using MediatR;

namespace Notes.Application.Features.Notes.UpdateNote;

public record UpdateNoteCommand(int NoteId, int UserId, string Title, string Content) : IRequest;