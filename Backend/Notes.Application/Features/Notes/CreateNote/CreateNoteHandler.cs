using Common.Application.Exceptions;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain.Entities;
using Shared.Contracts.Grpc;

namespace Notes.Application.Features.Notes.CreateNote;

public class CreateNoteHandler(UserVerificationGrpc.UserVerificationGrpcClient grpcClient, INoteRepository noteRepository) : IRequestHandler<CreateNoteCommand>
{
    public async Task Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var response = await grpcClient.GetUserVerificationAsync(new UserVerificationRequest() { UserId = request.UserId });
        if (!response.UserExists || !response.IsVerified)
            throw new UnauthorizedException("User does not exist or is not verified.");

        var note = new Note()
        {
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow
        };
        
        await noteRepository.CreateAsync(note);
    }
}