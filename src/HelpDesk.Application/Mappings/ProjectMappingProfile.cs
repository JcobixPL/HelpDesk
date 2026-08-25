using AutoMapper;
using HelpDesk.Application.DTOs.Projects;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Mappings;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectDto>();
    }
}
