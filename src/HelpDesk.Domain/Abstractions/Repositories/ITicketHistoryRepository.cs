using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Abstractions.Repositories;

public interface ITicketHistoryRepository
{
    Task<IReadOnlyList<TicketHistory>> GetByTicketIdAsync(
        Guid ticketId,
        CancellationToken cancellationToken);

    void Add(TicketHistory history);
}
