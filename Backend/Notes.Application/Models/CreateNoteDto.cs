namespace Notes.Application.Models;

public record CreateNoteDto(string Title, string Content, DateTime? CreatedAt);