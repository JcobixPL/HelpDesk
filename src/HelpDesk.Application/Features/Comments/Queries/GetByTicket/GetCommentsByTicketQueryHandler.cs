using AutoMapper;
using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Queries.GetByTicket;

public class GetCommentsByTicketQueryHandler : IRequestHandler<GetCommentsByTicketQuery, IReadOnlyList<CommentDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public GetCommentsByTicketQueryHandler(
        ICommentRepository commentRepository,
        ITicketRepository ticketRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CommentDto>> Handle(
        GetCommentsByTicketQuery request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);

        if (ticket is null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} was not found.");
        }

        var comments = await _commentRepository.GetByTicketIdAsync(
            request.TicketId,
            cancellationToken);

        return _mapper.Map<IReadOnlyList<CommentDto>>(comments);
    }
}
