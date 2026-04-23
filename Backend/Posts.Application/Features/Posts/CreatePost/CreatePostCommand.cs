using MediatR;

namespace Posts.Application.Features.Posts.CreatePost;

public record CreatePostCommand(int UserId, string Title, string Content, DateTime? CreatedAt) : IRequest;