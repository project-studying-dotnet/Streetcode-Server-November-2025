using AutoMapper;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.BLL.Mapping.Streetcode;

public class CommentsProfile : Profile
{
    public CommentsProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(
                dest => dest.Replies,
                opt => opt.MapFrom(src => src.Replies));

        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();
    }
}