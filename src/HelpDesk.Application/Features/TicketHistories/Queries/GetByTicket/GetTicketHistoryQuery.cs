using HelpDesk.Application.DTOs.TicketHistories;
using MediatR;

namespace HelpDesk.Application.Features.TicketHistories.Queries.GetByTicket;

public record GetTicketHistoryQuery(Guid TicketId) : IRequest<IReadOnlyList<TicketHistoryDto>>;