using AutoMapper;
using HelpDesk.Application.DTOs.Projects;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Queries.Get;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, IReadOnlyList<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProjectDto>> Handle(
        GetProjectQuery request,
        CancellationToken cancellationToken )
    {
        var projects = await _projectRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IReadOnlyList<ProjectDto>>(projects);
    }
}
