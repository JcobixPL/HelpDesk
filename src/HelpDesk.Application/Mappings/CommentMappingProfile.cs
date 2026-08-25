using AutoMapper;
using HelpDesk.Application.DTOs.Comments;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Mappings;

public class CommentMappingProfile :Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentDto>();
    }
}
