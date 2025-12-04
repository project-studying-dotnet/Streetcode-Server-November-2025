using AutoMapper;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.DTO.Media.Audio;
using Streetcode.BLL.DTO.Media.Images;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Entities.Streetcode.Types;
using Streetcode.DAL.Enums;

namespace Streetcode.BLL.Mapping.Streetcode;

public class StreetcodeProfile : Profile
{
    public StreetcodeProfile()
    {
        CreateMap<StreetcodeContent, StreetcodeDto>()
            .ForMember(x => x.StreetcodeType, conf => conf.MapFrom(s => GetStreetcodeType(s)))
            .ReverseMap();
        CreateMap<StreetcodeContent, StreetcodeShortDto>().ReverseMap();
        CreateMap<StreetcodeContent, StreetcodeMainPageDto>()
             .ForPath(dto => dto.Text, conf => conf
                .MapFrom(e => e.Text.Title))
            .ForPath(dto => dto.ImageId, conf => conf
                .MapFrom(e => e.Images.Select(i => i.Id).LastOrDefault()));

        CreateMap<CreateStreetcodeDto, StreetcodeContent>()
            .ForMember(x => x.Images, conf => conf.Ignore())
            .ForMember(sc => sc.Tags, conf => conf.Ignore())
            .ReverseMap();

        CreateMap<UpdateStreetcodeDto, StreetcodeContent>()
            .ForMember(x => x.Images, conf => conf.Ignore())
            .ForMember(sc => sc.Tags, conf => conf.Ignore())
            .ReverseMap();
    }

    private StreetcodeType GetStreetcodeType(StreetcodeContent streetcode)
    {
        if(streetcode is EventStreetcode)
        {
            return StreetcodeType.Event;
        }

        return StreetcodeType.Person;
    }
}
