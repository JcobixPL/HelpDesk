using HelpDesk.Application.DTOs.TicketHistories;
using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Application.Features.TicketHistories.Queries.GetByTicket;
using HelpDesk.Application.Features.Tickets.Commands.Assign;
using HelpDesk.Application.Features.Tickets.Commands.ChangePriority;
using HelpDesk.Application.Features.Tickets.Commands.ChangeStatus;
using HelpDesk.Application.Features.Tickets.Commands.Create;
using HelpDesk.Application.Features.Tickets.Commands.Unassign;
using HelpDesk.Application.Features.Tickets.Commands.Update;
using HelpDesk.Application.Features.Tickets.Queries.Get;
using HelpDesk.Application.Features.Tickets.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketController : ControllerBase
{
    private readonly ISender _sender;

    public TicketController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TicketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TicketDto>>> GetAll(
    CancellationToken cancellationToken)
    {
        var tickets = await _sender.Send(
            new GetTicketsQuery(),
            cancellationToken);

        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var ticket = await _sender.Send(
            new GetTicketByIdQuery(id),
            cancellationToken);

        return Ok(ticket);
    }

    [HttpGet("{id:guid}/history")]
    [ProducesResponseType(typeof(IReadOnlyList<TicketHistoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<TicketHistoryDto>>> GetHistory(
        Guid id,
        CancellationToken cancellationToken)
    {
        var history = await _sender.Send(
            new GetTicketHistoryQuery(id),
            cancellationToken);

        return Ok(history);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> Create(
    CreateTicketRequest request,
    CancellationToken cancellationToken)
    {
        var command = new CreateTicketCommand(
            request.Title,
            request.Description,
            request.Priority,
            request.Type,
            request.ProjectId);

        var ticket = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = ticket.Id },
            ticket);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> Update(
    Guid id,
    UpdateTicketRequest request,
    CancellationToken cancellationToken)
    {
        var command = new UpdateTicketCommand(
            id,
            request.Title,
            request.Description,
            request.Priority,
            request.Type);

        var ticket = await _sender.Send(
            command,
            cancellationToken);

        return Ok(ticket);
    }

    [HttpPatch("{id:guid}/assignee")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> Assign(
    Guid id,
    AssignTicketRequest request,
    CancellationToken cancellationToken)
    {
        var command = new AssignTicketCommand(
            id,
            request.UserId);

        var ticket = await _sender.Send(
            command,
            cancellationToken);

        return Ok(ticket);
    }

    [HttpDelete("{id:guid}/assignee")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> Unassign(
    Guid id,
    CancellationToken cancellationToken)
    {
        var ticket = await _sender.Send(
            new UnassignTicketCommand(id),
            cancellationToken);

        return Ok(ticket);
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> ChangeStatus(
    Guid id,
    ChangeTicketStatusRequest request,
    CancellationToken cancellationToken)
    {
        var command = new ChangeTicketStatusCommand(
            id,
            request.Status);

        var ticket = await _sender.Send(command, cancellationToken);

        return Ok(ticket);
    }

    [HttpPatch("{id:guid}/priority")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> ChangePriority(
    Guid id,
    ChangeTicketPriorityRequest request,
    CancellationToken cancellationToken)
    {
        var command = new ChangeTicketPriorityCommand(
            id,
            request.Priority);

        var ticket = await _sender.Send(command, cancellationToken);

        return Ok(ticket);
    }
}
