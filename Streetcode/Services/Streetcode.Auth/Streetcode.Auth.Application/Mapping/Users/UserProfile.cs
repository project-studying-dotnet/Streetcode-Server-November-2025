using AutoMapper;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Dtos.Users;
using Streetcode.Auth.Domain.Entities.Users;

namespace Streetcode.Auth.Application.Mapping.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, RegisterUserResponseDto>();
        }
    }
}
