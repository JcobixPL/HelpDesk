using AutoMapper;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Commands;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTicketCommandHandler(ITicketRepository ticketRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
            reporterId: request.ReporterId);

        _ticketRepository.Add(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TicketDto>(ticket);
    }
}
