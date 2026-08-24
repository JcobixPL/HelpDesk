using HelpDesk.Application.DTOs.Users;
using MediatR;

namespace HelpDesk.Application.Features.Users.Queries.Get;

public record GetUsersQuery : IRequest<IReadOnlyList<UserDto>>;
