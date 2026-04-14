using Notes.Domain.Entities;

namespace Notes.Application.Interfaces;

public interface INoteRepository
{
    Task CreateAsync(Note note);
    
    Task<Note?> GetAsync(int id, int userId);
    
    Task<bool> UpdateAsync(int id, int userId, string newTitle, string newContent);
    
    Task<bool> DeleteAsync(int id, int userId);
}