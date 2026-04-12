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

    public async Task<Note?> GetByTitleAsync(int userId, string title)
    {
        return await context.Notes.Where(note => note.UserId == userId).FirstOrDefaultAsync(note => note.Title == title);
    }
}