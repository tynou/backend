using MediatR;

namespace Posts.Application.Features.Posts.DeletePost;

public record DeletePostCommand(int PostId, int UserId) : IRequest;