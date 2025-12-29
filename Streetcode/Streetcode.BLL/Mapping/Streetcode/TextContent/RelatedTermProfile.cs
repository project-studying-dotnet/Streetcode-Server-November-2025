using AutoMapper;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.DAL.Entities.Streetcode.TextContent;

namespace Streetcode.BLL.Mapping.TextContent;

public class RelatedTermProfile : Profile
{
    public RelatedTermProfile()
    {
        CreateMap<RelatedTerm, RelatedTermDto>().ReverseMap();
     }
}
