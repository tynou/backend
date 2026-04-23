using MediatR;
using Posts.Application.Interfaces;

namespace Posts.Application.Features.Posts.UpdatePost;

public class UpdatePostHandler(IPostRepository postRepository) : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var success = await postRepository.UpdateAsync(request.PostId, request.UserId, request.Title, request.Content);

        if (!success)
            throw new Exception("Post could not be updated.");
    }
}