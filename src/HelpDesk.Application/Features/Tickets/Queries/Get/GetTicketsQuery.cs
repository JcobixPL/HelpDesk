using HelpDesk.Application.DTOs.Tickets;
using MediatR;

namespace HelpDesk.Application.Features.Tickets.Queries.Get;

public record GetTicketsQuery : IRequest<IReadOnlyList<TicketDto>>;