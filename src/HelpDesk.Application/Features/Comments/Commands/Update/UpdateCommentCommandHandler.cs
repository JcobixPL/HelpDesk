
using AutoMapper;
using HelpDesk.Application.Abstractions.Authentication;
using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Domain.Abstractions.Repositories;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Enums;
using MediatR;

namespace HelpDesk.Application.Features.Comments.Commands.Update;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITicketHistoryRepository _ticketHistoryRepository;

    public UpdateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ITicketHistoryRepository ticketHistoryRepository)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _ticketHistoryRepository = ticketHistoryRepository;
    }

    public async Task<CommentDto> Handle(
        UpdateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(request.Id);

        if (comment is null)
        {
            throw new KeyNotFoundException($"Comment with ID {request.Id} was not found.");
        }

        var oldContent = comment.Content;

        comment.Update(request.Content);

        var history = new TicketHistory(
            ticketId: comment.TicketId,
            userId: _currentUserService.UserId,
            action: TicketHistoryAction.CommentUpdated,
            oldValue: oldContent,
            newValue: comment.Content);

        _ticketHistoryRepository.Add(history);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentDto>(comment);
    }
}
