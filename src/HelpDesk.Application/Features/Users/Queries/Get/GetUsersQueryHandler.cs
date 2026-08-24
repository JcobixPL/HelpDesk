using AutoMapper;
using HelpDesk.Application.DTOs.Users;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using MediatR;

namespace HelpDesk.Application.Features.Users.Queries.Get;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IReadOnlyList<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IReadOnlyList<UserDto>>(users);
    }
}
