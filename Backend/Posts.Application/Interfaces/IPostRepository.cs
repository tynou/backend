using Posts.Domain.Entities;

namespace Posts.Application.Interfaces;

public interface IPostRepository
{
    Task CreateAsync(Post post);
    
    Task<Post?> GetAsync(int id, int userId);
    
    Task<bool> UpdateAsync(int id, int userId, string newTitle, string newContent);
    
    Task<bool> DeleteAsync(int id, int userId);
}