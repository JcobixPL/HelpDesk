using AutoMapper;
using HelpDesk.Application.DTOs.TicketHistories;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Mappings;

public class TicketHistoryMappingProfile : Profile
{
    public TicketHistoryMappingProfile()
    {
        CreateMap<TicketHistory, TicketHistoryDto>();
    }
}
