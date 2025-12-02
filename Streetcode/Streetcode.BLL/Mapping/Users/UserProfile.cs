using AutoMapper;
using Streetcode.BLL.DTO.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserLoginDtoo>().ReverseMap();
            CreateMap<UserDtoo, UserLoginDtoo>().ReverseMap();
            CreateMap<User, UserDtoo>().ReverseMap();
        }
    }
}
