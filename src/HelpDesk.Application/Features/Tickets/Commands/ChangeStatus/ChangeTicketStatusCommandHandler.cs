using AutoMapper;
using HelpDesk.Application.Abstractions.Authentication;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.ChangeStatus;

public class ChangeTicketStatusCommandHandler : IRequestHandler<ChangeTicketStatusCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketHistoryRepository _ticketHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public ChangeTicketStatusCommandHandler(
        ITicketRepository ticketRepository,
        ITicketHistoryRepository ticketHistoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _ticketRepository = ticketRepository;
        _ticketHistoryRepository = ticketHistoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<TicketDto> Handle(
        ChangeTicketStatusCommand request,
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

        var oldStatus = ticket.Status;

        ticket.ChangeStatus(request.Status);

        var history = new TicketHistory(
            ticketId: ticket.Id,
            userId: _currentUserService.UserId,
            action: TicketHistoryAction.StatusChanged,
            oldValue: oldStatus.ToString(),
            newValue: ticket.Status.ToString());

        _ticketHistoryRepository.Add(history);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}