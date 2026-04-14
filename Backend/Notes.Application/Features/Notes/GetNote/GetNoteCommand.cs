using MediatR;
using Notes.Application.Models;

namespace Notes.Application.Features.Notes.GetNote;

public record GetNoteCommand(int NoteId, int UserId) : IRequest<NoteDto>;