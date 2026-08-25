using AutoMapper;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Queries.GetById;

public class GetTicketByIdQueryHandler
    : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public GetTicketByIdQueryHandler(
        ITicketRepository ticketRepository,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<TicketDto> Handle(
        GetTicketByIdQuery request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (ticket is null)
        {
            throw new KeyNotFoundException(
                $"Ticket with ID {request.Id} not found.");
        }

        return _mapper.Map<TicketDto>(ticket);
    }
}