using AutoMapper;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Update;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTicketCommandHandler(
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TicketDto> Handle(
        UpdateTicketCommand request,
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

        ticket.Update(
            request.Title,
            request.Description,
            request.Priority,
            request.Type);

        _ticketRepository.Update(ticket);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}