using AutoMapper;
using HelpDesk.Application.Abstractions.Authentication;
using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Commands.Create;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITicketHistoryRepository _ticketHistoryRepository;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        ITicketRepository ticketRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ITicketHistoryRepository ticketHistoryRepository)
    {
        _commentRepository = commentRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _ticketHistoryRepository = ticketHistoryRepository;
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

        var comment = new Comment(
            request.Content,
            request.TicketId,
            _currentUserService.UserId);

        _commentRepository.Add(comment);

        var history = new TicketHistory(
            ticketId: comment.TicketId,
            userId: _currentUserService.UserId,
            action: TicketHistoryAction.CommentAdded,
            newValue: comment.Content);

        _ticketHistoryRepository.Add(history);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentDto>(comment);
    }
}
