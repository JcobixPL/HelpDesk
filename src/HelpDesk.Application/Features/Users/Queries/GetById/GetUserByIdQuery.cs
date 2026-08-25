using HelpDesk.Application.DTOs.Users;
using MediatR;

namespace HelpDesk.Application.Features.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
