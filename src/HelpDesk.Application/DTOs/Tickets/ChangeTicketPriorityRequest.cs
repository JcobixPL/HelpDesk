using HelpDesk.Domain.Enums;

namespace HelpDesk.Application.DTOs.Tickets;

public record ChangeTicketPriorityRequest(TicketPriority Priority);