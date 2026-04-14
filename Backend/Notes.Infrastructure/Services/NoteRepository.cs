using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain.Entities;
using Notes.Infrastructure.Persistence;

namespace Notes.Infrastructure.Services;

public class NoteRepository(NoteDbContext context) : INoteRepository
{
    public async Task CreateAsync(Note note)
    {
        context.Notes.Add(note);
        await context.SaveChangesAsync();
    }

    public async Task<Note?> GetAsync(int id, int userId)
    {
        return await context.Notes.FirstOrDefaultAsync(note => note.Id == id && note.UserId == userId);
    }

    public async Task<bool> UpdateAsync(int id, int userId, string newTitle, string newContent)
    {
        var note = await GetAsync(id, userId);
        if (note is null)
            return false;
        
        note.Title = newTitle;
        note.Content = newContent;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var note = await GetAsync(id,  userId);
        if (note is null)
            return false;
        
        context.Notes.Remove(note);
        await context.SaveChangesAsync();
        return true;
    }
}