using AutoMapper;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Queries.Get;

public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, IReadOnlyList<TicketDto>>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public GetTicketsQueryHandler(
        ITicketRepository ticketRepository,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<TicketDto>> Handle(
        GetTicketsQuery request,
        CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.GetAllAsync(
            cancellationToken);

        return _mapper.Map<IReadOnlyList<TicketDto>>(tickets);
    }
}