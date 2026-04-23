namespace Posts.Application.Models;

public record CreatePostDto(string Title, string Content, DateTime? CreatedAt);