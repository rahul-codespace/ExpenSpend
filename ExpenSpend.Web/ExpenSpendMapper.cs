using AutoMapper;
using ExpenSpend.Domain.DTOs.Expenses;
using ExpenSpend.Domain.DTOs.Friends;
using ExpenSpend.Domain.DTOs.GroupMembers;
using ExpenSpend.Domain.DTOs.Groups;
using ExpenSpend.Domain.DTOs.Payments;
using ExpenSpend.Domain.DTOs.Users;
using ExpenSpend.Domain.Models.Expenses;
using ExpenSpend.Domain.Models.Friends;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
using ExpenSpend.Domain.Models.Payments;
using ExpenSpend.Domain.Models.Users;

namespace ExpenSpend.Web
{
    public class ExpenSpendMapper : Profile
    {
        public ExpenSpendMapper()
        {
            CreateMap<ApplicationUser, CreateUserDto>().ForMember(dest => dest.Password, opt => opt.Ignore()).ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();
            CreateMap<ApplicationUser, GetUserDto>();

            // friend
            CreateMap<Friendship, GetFriendshipDto>();
            CreateMap<CreateFriendshipDto, Friendship>();

            // group
            CreateMap<Group, GetGroupDto>();
            CreateMap<GroupMember, GetGroupMemberDto>();
            CreateMap<CreateGroupDto, Group>();

            // expense 
            CreateMap<Expense, GetExpenseDto>();
            CreateMap<CreateExpenseDto, Expense>();
            CreateMap<UpdateExpenseDto, Expense>();

            // payment
            CreateMap<Payment, GetPaymentDto>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<UpdatePaymentDto, Payment>();

        }
    }
}
