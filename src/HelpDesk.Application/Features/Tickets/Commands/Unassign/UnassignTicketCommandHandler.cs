using AutoMapper;
using HelpDesk.Application.Abstractions.Authentication;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Unassign;

public class UnassignTicketCommandHandler : IRequestHandler<UnassignTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITicketHistoryRepository _ticketHistoryRepository;
    private readonly ICurrentUserService _currentUserService;

    public UnassignTicketCommandHandler(
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITicketHistoryRepository ticketHistoryRepository,
        ICurrentUserService currentUserService)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ticketHistoryRepository = ticketHistoryRepository;
        _currentUserService = currentUserService;
    }

    public async Task<TicketDto> Handle(
        UnassignTicketCommand request,
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

        var oldAssigneeId = ticket.AssigneeId;

        ticket.Unassign();

        var history = new TicketHistory(
            ticketId: ticket.Id,
            userId: _currentUserService.UserId,
            action: TicketHistoryAction.Unassigned,
            oldValue: oldAssigneeId?.ToString(),
            newValue: null);

        _ticketHistoryRepository.Add(history);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}