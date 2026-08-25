using AutoMapper;
using HelpDesk.Application.Features.Projects.Commands.Create;
using HelpDesk.Application.Mappings;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Tests.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateProjectAndReturnProjectDto()
    {
        // Arrange
        var projectRepository = new Mock<IProjectRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var services = new ServiceCollection();

        services.AddLogging();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ProjectMappingProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();

        var mapper = serviceProvider.GetRequiredService<IMapper>();

        var handler = new CreateProjectCommandHandler(
            projectRepository.Object,
            unitOfWork.Object,
            mapper);

        var createdById = Guid.NewGuid();

        var command = new CreateProjectCommand(
            "Projekt",
            "P",
            "Projekt 1",
            createdById);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Projekt", result.Name);
        Assert.Equal("P", result.Key);
        Assert.Equal("Projekt 1", result.Description);
        Assert.Equal(createdById, result.CreatedById);

        projectRepository.Verify(
            x => x.Add(It.Is<Project>(p =>
                p.Name == "Projekt" &&
                p.Key == "P" &&
                p.Description == "Projekt 1" &&
                p.CreatedById == createdById)),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
