using AutoMapper;
using HelpDesk.Application.Abstractions.Authentication;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Assign;

public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITicketHistoryRepository _ticketHistoryRepository;
    private readonly ICurrentUserService _currentUserService;

    public AssignTicketCommandHandler(
        ITicketRepository ticketRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITicketHistoryRepository ticketHistoryRepository,
        ICurrentUserService currentUserService)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ticketHistoryRepository = ticketHistoryRepository;
        _currentUserService = currentUserService;
    }

    public async Task<TicketDto> Handle(
        AssignTicketCommand request,
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

        var user = await _userRepository.GetByIdAsync(
            request.UserId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                $"User with ID {request.UserId} not found.");
        }

        var oldAssigneeId = ticket.AssigneeId;

        ticket.AssignTo(request.UserId);

        var history = new TicketHistory(
            ticketId: ticket.Id,
            userId: _currentUserService.UserId,
            action: TicketHistoryAction.Assigned,
            oldValue: oldAssigneeId?.ToString(),
            newValue: request.UserId.ToString());

        _ticketHistoryRepository.Add(history);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}