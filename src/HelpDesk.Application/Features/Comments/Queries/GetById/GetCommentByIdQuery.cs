using HelpDesk.Application.DTOs.Comments;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Queries.GetById;

public record GetCommentByIdQuery(Guid Id) : IRequest<CommentDto>;