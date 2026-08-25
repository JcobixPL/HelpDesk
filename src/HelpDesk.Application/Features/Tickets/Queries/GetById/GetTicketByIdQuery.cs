using HelpDesk.Application.DTOs.Tickets;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Queries.GetById;

public record GetTicketByIdQuery(Guid Id) : IRequest<TicketDto>;
