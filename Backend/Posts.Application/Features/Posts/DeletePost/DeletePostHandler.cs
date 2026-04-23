using MediatR;
using Posts.Application.Interfaces;

namespace Posts.Application.Features.Posts.DeletePost;

public class DeletePostHandler(IPostRepository postRepository) : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var success = await postRepository.DeleteAsync(request.PostId, request.UserId);

        if (!success)
            throw new Exception("Post could not be deleted.");
    }
}