using AutoMapper;
using Streetcode.BLL.DTO.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<UserDto, UserLoginDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserResponseDto>().ReverseMap();
            CreateMap<RegisterUserDto, User>().ReverseMap();
        }
    }
}
