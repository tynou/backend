using Microsoft.EntityFrameworkCore;
using Posts.Application.Interfaces;
using Posts.Domain.Entities;
using Posts.Infrastructure.Persistence;

namespace Posts.Infrastructure.Services;

public class PostRepository(PostDbContext context) : IPostRepository
{
    public async Task CreateAsync(Post post)
    {
        context.Notes.Add(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetAsync(int id, int userId)
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