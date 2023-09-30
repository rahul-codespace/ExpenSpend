using AutoMapper;
using ExpenSpend.Core.User;
using ExpenSpend.Domain.Models.Users;

namespace ExpenSpend.Web
{
    public class ExpenSpendMapper : Profile
    {
        public ExpenSpendMapper()
        {
            CreateMap<ESUser, CreateUserDto>().ForMember(dest => dest.Password, opt => opt.Ignore()).ReverseMap();
            CreateMap<ESUser, UpdateUserDto>().ReverseMap();
            CreateMap<ESUser, GetUserDto>();
        }
    }
}
