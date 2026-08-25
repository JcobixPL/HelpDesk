using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Abstractions.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Comment>> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken= default);
    void Add(Comment comment);
    void Update(Comment comment);
}
