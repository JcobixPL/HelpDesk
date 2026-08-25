using AutoMapper;
using HelpDesk.Application.DTOs.Projects;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Queries.GetById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(
        GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project is null)
        {
            throw new KeyNotFoundException($"Project with id {request.Id} was not found.");
        }

        return _mapper.Map<ProjectDto>(project);
    }
}
