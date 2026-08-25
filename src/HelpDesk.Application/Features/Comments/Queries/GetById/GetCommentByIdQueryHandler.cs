using AutoMapper;
using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Domain.Abstractions.Repositories;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Queries.GetById;

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public GetCommentByIdQueryHandler(
        ICommentRepository commentRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(
        GetCommentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (comment is null)
        {
            throw new KeyNotFoundException(
                $"Comment with ID {request.Id} not found.");
        }

        return _mapper.Map<CommentDto>(comment);
    }
}
