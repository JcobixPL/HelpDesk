using HelpDesk.Application.DTOs.Comments;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Commands.Create;

public record CreateCommentCommand(
    string Content,
    Guid TicketId) : IRequest<CommentDto>;
