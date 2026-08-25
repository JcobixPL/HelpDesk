using AutoMapper;
using HelpDesk.Application.DTOs.Projects;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository,  IUnitOfWork unitOfWork, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(
        UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
        {
            throw new KeyNotFoundException($"Project with id {request.Id} was not found.");
        }

        project.Update(request.Name, request.Description);

        _projectRepository.Update(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<ProjectDto>(project);
    }
}
