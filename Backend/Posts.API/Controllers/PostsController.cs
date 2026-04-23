using Common.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posts.Application.Features.Posts.CreatePost;
using Posts.Application.Features.Posts.DeletePost;
using Posts.Application.Features.Posts.GetPost;
using Posts.Application.Features.Posts.UpdatePost;
using Posts.Application.Models;

namespace Posts.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var command = new GetPostCommand(id, User.GetUserId());

        var post = await mediator.Send(command);
        
        return Ok(post);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        var command = new CreatePostCommand(
            User.GetUserId(),
            createPostDto.Title,
            createPostDto.Content,
            createPostDto.CreatedAt
        );

        await mediator.Send(command);

        return Created();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDto updatePostDto)
    {
        var command = new UpdatePostCommand(id, User.GetUserId(), updatePostDto.Title, updatePostDto.Content);

        await mediator.Send(command);
        
        return Ok("Post successfully updated.");
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var command = new DeletePostCommand(id, User.GetUserId());
        
        await mediator.Send(command);

        return Ok("Post successfully deleted.");
    }
}