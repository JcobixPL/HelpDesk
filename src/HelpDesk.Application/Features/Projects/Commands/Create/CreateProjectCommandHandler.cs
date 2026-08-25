using AutoMapper;
using HelpDesk.Application.DTOs.Projects;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IProjectRepository projectRepository,  IUnitOfWork unitOfWork, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = new Project(
            request.Name,
            request.Key,
            request.Description,
            request.CreatedById);

        _projectRepository.Add(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProjectDto>(project);
    }
}
