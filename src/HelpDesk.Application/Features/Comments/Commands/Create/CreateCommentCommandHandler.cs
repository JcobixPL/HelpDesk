using AutoMapper;
using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Commands.Create;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        ITicketRepository ticketRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken);

        if (ticket is null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} was not found.");
        }

        var author = await _userRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (author is null)
        {
            throw new KeyNotFoundException($"Author with ID {request.AuthorId} was not found.");
        }

        var comment = new Comment(
            request.Content,
            request.TicketId,
            request.AuthorId);

        _commentRepository.Add(comment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentDto>(comment);
    }
}
