using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Persistence;

public class NoteDbContext(DbContextOptions<NoteDbContext> options) : DbContext(options)
{
    public virtual DbSet<Note> Notes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoteDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}