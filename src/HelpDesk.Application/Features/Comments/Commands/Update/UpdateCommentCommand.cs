using HelpDesk.Application.DTOs.Comments;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Commands.Update;

public record UpdateCommentCommand(
    Guid Id,
    string Content) : IRequest<CommentDto>;

