using HelpDesk.Application.DTOs.Comments;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Queries.GetByTicket;

public record GetCommentsByTicketQuery(Guid TicketId) : IRequest<IReadOnlyList<CommentDto>>;
