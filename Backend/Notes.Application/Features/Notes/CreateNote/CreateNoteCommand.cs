using MediatR;

namespace Notes.Application.Features.Notes.CreateNote;

public record CreateNoteCommand(int UserId, string Title, string Content, DateTime? CreatedAt) : IRequest;