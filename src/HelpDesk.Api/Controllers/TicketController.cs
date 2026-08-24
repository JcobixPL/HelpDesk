using HelpDesk.Application.DTOs.Tickets;
using HelpDesk.Application.Features.Tickets.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ISender _sender;

    public TicketController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TicketDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> Create(
        CreateTicketCommand command,
        CancellationToken cancellationToken)
    {
        var ticket = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }
}
