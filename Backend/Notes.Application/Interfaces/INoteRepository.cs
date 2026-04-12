using Notes.Domain.Entities;

namespace Notes.Application.Interfaces;

public interface INoteRepository
{
    Task CreateAsync(Note note);
    
    Task<Note?> GetByTitleAsync(int userId, string title);
}