using AutoMapper;
using HelpDesk.Application.Abstractions.Authentication;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands.Create;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITicketHistoryRepository _ticketHistoryRepository;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, IUserRepository userRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, ITicketHistoryRepository ticketHistoryRepository)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _ticketHistoryRepository = ticketHistoryRepository;
    }

    public async Task<TicketDto> Handle(
        CreateTicketCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);

        if (project is null)
        {
            throw new KeyNotFoundException($"Project with ID {request.ProjectId} not found.");
        }

        int ticketNumber = project.GetNextTicketNumber();
        var ticketKey = $"{project.Key}-{ticketNumber}";

        var ticket = new Ticket(
            key: ticketKey,
            title: request.Title,
            description: request.Description,
            priority: request.Priority,
            type: request.Type,
            projectId: request.ProjectId,
            reporterId: _currentUserService.UserId);

        _ticketRepository.Add(ticket);

        var history = new TicketHistory(
            ticketId: ticket.Id,
            userId: _currentUserService.UserId,
            action: TicketHistoryAction.Created,
            oldValue: null,
            newValue: null);

        _ticketHistoryRepository.Add(history);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}
