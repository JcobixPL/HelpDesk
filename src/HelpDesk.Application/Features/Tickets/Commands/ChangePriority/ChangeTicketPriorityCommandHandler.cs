using AutoMapper;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.ChangePriority;

public class ChangeTicketPriorityCommandHandler : IRequestHandler<ChangeTicketPriorityCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChangeTicketPriorityCommandHandler(
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TicketDto> Handle(
        ChangeTicketPriorityCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(
            request.TicketId,
            cancellationToken);

        if (ticket is null)
        {
            throw new KeyNotFoundException(
                $"Ticket with ID {request.TicketId} not found.");
        }

        ticket.ChangePriority(request.Priority);

        _ticketRepository.Update(ticket);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}