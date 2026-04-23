using MediatR;
using Posts.Application.Models;

namespace Posts.Application.Features.Posts.GetPost;

public record GetPostCommand(int PostId, int UserId) : IRequest<PostDto>;