using MediatR;

namespace Posts.Application.Features.Posts.UpdatePost;

public record UpdatePostCommand(int PostId, int UserId, string Title, string Content) : IRequest;