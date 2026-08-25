using AutoMapper;
using HelpDesk.Application.DTOs.TicketHistories;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.TicketHistories.Queries.GetByTicket;

public class GetTicketHistoryQueryHandler : IRequestHandler<GetTicketHistoryQuery, IReadOnlyList<TicketHistoryDto>>
{
    private readonly ITicketHistoryRepository _ticketHistoryRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public GetTicketHistoryQueryHandler(
        ITicketHistoryRepository ticketHistoryRepository,
        ITicketRepository ticketRepository,
        IMapper mapper)
    {
        _ticketHistoryRepository = ticketHistoryRepository;
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<TicketHistoryDto>> Handle(
        GetTicketHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);

        if (ticket is null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} was not found.");
        }

        var history = _ticketHistoryRepository.GetByTicketIdAsync(request.TicketId, cancellationToken);

        return _mapper.Map<IReadOnlyList<TicketHistoryDto>>(history);
    }
}
