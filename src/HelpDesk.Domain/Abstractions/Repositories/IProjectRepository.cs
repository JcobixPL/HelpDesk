using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Abstractions.Repositories;

public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Project project);
    void Update(Project project);
}
