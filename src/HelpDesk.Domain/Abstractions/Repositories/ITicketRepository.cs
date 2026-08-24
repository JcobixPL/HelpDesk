using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Abstractions.Repositories;

public interface ITicketRepository
{
    Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Ticket ticket);
    void Update(Ticket ticket);
}
