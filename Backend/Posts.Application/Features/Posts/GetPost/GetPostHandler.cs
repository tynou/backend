using MediatR;
using Posts.Application.Interfaces;
using Posts.Application.Models;

namespace Posts.Application.Features.Posts.GetPost;

public class GetPostHandler(IPostRepository postRepository) : IRequestHandler<GetPostCommand, PostDto>
{
    public async Task<PostDto> Handle(GetPostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetAsync(request.PostId, request.UserId);
        if (post is null)
            throw new Exception("Post not found.");

        var postDto = new PostDto(
            post.Id,
            post.Title,
            post.Content,
            post.CreatedAt
        );

        return postDto;
    }
}