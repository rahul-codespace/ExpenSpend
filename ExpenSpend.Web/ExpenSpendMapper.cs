using AutoMapper;
using ExpenSpend.Domain.DTOs.Friends;
using ExpenSpend.Domain.DTOs.GroupMembers;
using ExpenSpend.Domain.DTOs.Groups;
using ExpenSpend.Domain.DTOs.Users;
using ExpenSpend.Domain.Models.Friends;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
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
            CreateMap<Friendship, GetFriendshipDto>();
            CreateMap<CreateFriendshipDto, Friendship>();
            CreateMap<Group, GetGroupDto>();
            CreateMap<GroupMember, GetGroupMemberDto>();
        }
    }
}
