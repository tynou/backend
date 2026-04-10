using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Persistence;

public class NotesDbContext(DbContextOptions<NotesDbContext> options) : DbContext(options)
{
    public virtual DbSet<Note> Notes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}