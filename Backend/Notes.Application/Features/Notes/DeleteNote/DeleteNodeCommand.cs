using MediatR;

namespace Notes.Application.Features.Notes.DeleteNote;

public record DeleteNodeCommand(int NoteId, int UserId) : IRequest;