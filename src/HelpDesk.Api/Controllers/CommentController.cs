using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Application.Features.Comments.Commands.Create;
using HelpDesk.Application.Features.Comments.Commands.Update;
using HelpDesk.Application.Features.Comments.Queries.GetById;
using HelpDesk.Application.Features.Comments.Queries.GetByTicket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api")]
public class CommentController : ControllerBase
{
    private readonly ISender _sender;

    public CommentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("comments/{id:guid}")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var comment = _sender.Send(
            new GetCommentByIdQuery(id),
            cancellationToken);

        return Ok(comment);
    }

    [HttpGet("tickets/{ticketId:guid}/comments")]
    [ProducesResponseType(typeof(IReadOnlyList<CommentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetByTicket(
        Guid ticketId,
        CancellationToken cancellationToken)
    {
        var comments = await _sender.Send(
            new GetCommentsByTicketQuery(ticketId),
            cancellationToken);

        return Ok(comments);
    }

    [HttpPost("tickets/{ticketId:guid}/comments")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> Create(
        Guid ticketId,
        CreateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommentCommand(
            request.Content,
            ticketId,
            request.AuthorId);

        var comment = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = comment.Id },
            comment);
    }

    [HttpPut("comments/{id:guid}")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> Update(
        Guid id,
        UpdateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommentCommand(
            id,
            request.Content);

        var comment = await _sender.Send(
            command,
            cancellationToken);

        return Ok(comment);
    }
}
