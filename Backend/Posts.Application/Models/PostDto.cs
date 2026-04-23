namespace Posts.Application.Models;

public record PostDto(int Id, string Title, string Content, DateTime CreatedAt);