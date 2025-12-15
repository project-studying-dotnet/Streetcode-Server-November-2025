using AutoMapper;
using Streetcode.BLL.DTO.Timeline;
using Streetcode.DAL.Entities.Timeline;

namespace Streetcode.BLL.Mapping.Timeline;

public class TimelineItemProfile : Profile
{
    public TimelineItemProfile()
    {
        CreateMap<TimelineItem, TimelineItemDto>()
            .ForMember(dest => dest.HistoricalContexts, opt => opt.MapFrom(x => x.HistoricalContextTimelines
                .Select(x => new HistoricalContextDto
                {
                    Id = x.HistoricalContextId,
                    Title = x.HistoricalContext.Title
                }).ToList()));

        CreateMap<CreateTimelineItemDto, TimelineItem>()
            .ForMember(dest => dest.HistoricalContextTimelines, opt => opt.Ignore());

        CreateMap<UpdateTimelineItemDto, TimelineItem>()
            .ForMember(dest => dest.HistoricalContextTimelines, opt => opt.Ignore())
            .ForMember(dest => dest.Streetcode, opt => opt.Ignore());

        CreateMap<CreateHistoricalContextDto, HistoricalContext>();
        CreateMap<UpdateHistoricalContextDto, HistoricalContext>()
            .ForMember(dest => dest.HistoricalContextTimelines, opt => opt.Ignore());
        CreateMap<HistoricalContext, HistoricalContextDto>();
    }
}
