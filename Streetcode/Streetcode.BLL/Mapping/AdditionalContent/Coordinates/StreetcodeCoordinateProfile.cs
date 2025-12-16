using AutoMapper;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;

namespace Streetcode.BLL.Mapping.AdditionalContent.Coordinates;

public class StreetcodeCoordinateProfile : Profile
{
   public StreetcodeCoordinateProfile()
   {
       CreateMap<StreetcodeCoordinate, StreetcodeCoordinateDto>()
           .ForMember(dto => dto.QrId, opt => opt.MapFrom(src => src.StatisticRecord != null ? src.StatisticRecord.QrId : (int?)null))
           .ForMember(dto => dto.Address, opt => opt.MapFrom(src => src.StatisticRecord != null ? src.StatisticRecord.Address : null))
           .ReverseMap();
   }
}