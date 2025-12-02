using AutoMapper;
using Streetcode.BLL.DTO.AdditionalContent;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.DAL.Entities.AdditionalContent;

namespace Streetcode.BLL.Mapping.AdditionalContent;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDtoo>().ForMember(x => x.Streetcodes, conf => conf.Ignore());
        CreateMap<Tag, StreetcodeTagDtoo>().ReverseMap();
        CreateMap<StreetcodeTagIndex, StreetcodeTagDtoo>()
            .ForMember(x => x.Id, conf => conf.MapFrom(ti => ti.TagId))
            .ForMember(x => x.Title, conf => conf.MapFrom(ti => ti.Tag.Title ?? ""));
    }
}
