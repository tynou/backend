using Common.Application.Exceptions;
using Common.Contracts.Grpc;
using MediatR;
using Posts.Application.Interfaces;
using Posts.Domain.Entities;

namespace Posts.Application.Features.Posts.CreatePost;

public class CreatePostHandler(UserVerificationGrpc.UserVerificationGrpcClient grpcClient, IPostRepository postRepository) : IRequestHandler<CreatePostCommand>
{
    public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        // понимаю, что использовать gRPC тут - это слишком
        // просто хотел опробовать технологию
        var response = await grpcClient.GetUserVerificationAsync(new UserVerificationRequest() { UserId = request.UserId });
        if (!response.UserExists || !response.IsVerified)
            throw new UnauthorizedException("User does not exist or is not verified.");

        var post = new Post()
        {
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };
        
        await postRepository.CreateAsync(post);
    }
}