using AutoMapper;
using ExpenSpend.Core.DTOs.Friends;
using ExpenSpend.Core.DTOs.Friends.Enums;
using ExpenSpend.Core.Helpers;
using ExpenSpend.Data.Context;
using ExpenSpend.Domain;
using ExpenSpend.Domain.Models.Friends;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExpenSpend.Service
{
    public class FriendAppService : IFriendAppService
    {
        private readonly IExpenSpendRepository<Friendship> _friendRepository;
        private readonly ExpenSpendDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public FriendAppService(
            IExpenSpendRepository<Friendship> friendRepository, 
            ExpenSpendDbContext context, IMapper mapper,
            IHttpContextAccessor httpContext
        ){
            _friendRepository = friendRepository;
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<ApiResponse<List<GetFriendshipDto>>> GetAllFriendsAsync()
        {
            var friends = await _friendRepository.GetAllAsync();
            return new ApiResponse<List<GetFriendshipDto>>
            {
                Data = _mapper.Map<List<GetFriendshipDto>>(friends),
                Message = "Friends found successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<List<GetFriendshipDto>>> GetFriendshipRequestsAsync()
        {
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            var friendRequests = await _context.Friendships
            .Where(f => f.RecipientId == currUser.Id && f.Status == FriendshipStatus.Pending)
            .Include(f => f.Initiator)
            .Include(f => f.Recipient)
            .ToListAsync();
            return new ApiResponse<List<GetFriendshipDto>>
            {
                Data = _mapper.Map<List<GetFriendshipDto>>(friendRequests),
                Message = "Friendship requests found successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<List<GetFriendshipDto>>> GetFriendshipsAsync()
        {
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            var friends = await _context.Friendships
            .Where(f => (f.InitiatorId == currUser.Id || f.RecipientId == currUser.Id) && f.Status == FriendshipStatus.Accepted)
            .Include(f => f.Initiator)
            .Include(f => f.Recipient)
            .ToListAsync();
            return new ApiResponse<List<GetFriendshipDto>>
            {
                Data = _mapper.Map<List<GetFriendshipDto>>(friends),
                Message = "Friends found successfully",
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<GetFriendshipDto>> GetFriendByIdAsync(Guid id)
        {
            var friend = await _friendRepository.GetByIdAsync(id);
            if (friend == null)
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friend not found",
                    StatusCode = 404
                };
            }
            return new ApiResponse<GetFriendshipDto>
            {
                Data = _mapper.Map<GetFriendshipDto>(friend),
                Message = "Friend found successfully",
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<GetFriendshipDto>> CreateFriendAsync(Guid recipientId)
        {
            var currentUser = _httpContext.HttpContext?.User?.Identity?.Name;
            var currUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == currentUser);
            var friend = new Friendship
            {
                InitiatorId = currUser.Id,
                RecipientId = recipientId,
                Status = FriendshipStatus.Pending,
                CreatedAt = DateTime.Now,
                CreatedBy = currUser.Id
            };
            var createdFriend = await _friendRepository.CreateAsync(friend);
            if (createdFriend == null)
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetFriendshipDto>
            {
                Data = _mapper.Map<GetFriendshipDto>(createdFriend),
                Message = "Friend created successfully",
                StatusCode = 201
            };
        }
        public async Task<ApiResponse<GetFriendshipDto>> UpdateFriendAsync(Friendship friendship)
        {
            var friendToUpdate = await _friendRepository.GetByIdAsync(friendship.Id);
            if (friendToUpdate == null)
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friend not found",
                    StatusCode = 404
                };
            }
            var result = await _friendRepository.UpdateAsync(_mapper.Map<Friendship>(friendToUpdate));
            if (result == null)
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetFriendshipDto>
            {
                Data = _mapper.Map<GetFriendshipDto>(friendship),
                Message = "Friend updated successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<GetFriendshipDto>> SoftDeleteFriendAsync(Guid Id)
        {
            var friend = await _friendRepository.GetByIdAsync(Id);
            if (friend == null)
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friend not found",
                    StatusCode = 404
                };
            }
            friend.IsDeleted = true;
            var deletedFriend = await _friendRepository.UpdateAsync(friend);
            if (deletedFriend == null)
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Bad Request",
                    StatusCode = 400
                };
            }
            return new ApiResponse<GetFriendshipDto>
            {
                Data = _mapper.Map<GetFriendshipDto>(deletedFriend),
                Message = "Friend deleted successfully",
                StatusCode = 200
            };
        }
        public async Task<ApiResponse<bool>> DeleteFriendAsync(Guid id)
        {
            var result = await _friendRepository.DeleteAsync(id);
            if (!result)
            {
                return new ApiResponse<bool>
                {
                    Data = false,
                    Message = "Bad Request or Friend not found!",
                    StatusCode = 400
                };
            }
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Friend deleted successfully",
                StatusCode = 200
            };
        }

        public async Task<ApiResponse<GetFriendshipDto>> AcceptAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.GetByIdAsync(friendshipId);
            if (friendship != null)
            {
                friendship.Status = FriendshipStatus.Accepted;
                await _friendRepository.UpdateAsync(friendship);
                return new ApiResponse<GetFriendshipDto>
                {
                    Data = _mapper.Map<GetFriendshipDto>(friendship),
                    Message = "Friendship accepted successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friendship not found",
                    StatusCode = 404
                };
            }
        }
        public async Task<ApiResponse<GetFriendshipDto>> DeclineAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.GetByIdAsync(friendshipId);
            if (friendship != null)
            {
                friendship.Status = FriendshipStatus.Declined;
                await _friendRepository.UpdateAsync(friendship);
                return new ApiResponse<GetFriendshipDto>
                {
                    Data = _mapper.Map<GetFriendshipDto>(friendship),
                    Message = "Friendship declined successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friendship not found",
                    StatusCode = 404
                };
            }
        }
        public async Task<ApiResponse<GetFriendshipDto>> BlockAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.GetByIdAsync(friendshipId);
            if (friendship != null)
            {
                friendship.Status = FriendshipStatus.Blocked;
                await _friendRepository.UpdateAsync(friendship);
                return new ApiResponse<GetFriendshipDto>
                {
                    Data = _mapper.Map<GetFriendshipDto>(friendship),
                    Message = "Friendship blocked successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friendship not found",
                    StatusCode = 404
                };
            }
        }
        public async Task<ApiResponse<GetFriendshipDto>> UnBlockAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.GetByIdAsync(friendshipId);
            if (friendship != null)
            {
                friendship.Status = FriendshipStatus.Accepted;
                await _friendRepository.UpdateAsync(friendship);
                return new ApiResponse<GetFriendshipDto>
                {
                    Data = _mapper.Map<GetFriendshipDto>(friendship),
                    Message = "Friendship unblocked successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ApiResponse<GetFriendshipDto>
                {
                    Message = "Friendship not found",
                    StatusCode = 404
                };
            }
        }
    }
}
