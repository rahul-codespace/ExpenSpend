using AutoMapper;
using ExpenSpend.Core.DTOs.Friends;
using ExpenSpend.Core.DTOs.GroupMembers;
using ExpenSpend.Core.DTOs.Groups;
using ExpenSpend.Core.DTOs.Users;
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
