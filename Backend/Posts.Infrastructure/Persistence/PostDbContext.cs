using Microsoft.EntityFrameworkCore;
using Posts.Domain.Entities;

namespace Posts.Infrastructure.Persistence;

public class PostDbContext(DbContextOptions<PostDbContext> options) : DbContext(options)
{
    public virtual DbSet<Post> Notes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}