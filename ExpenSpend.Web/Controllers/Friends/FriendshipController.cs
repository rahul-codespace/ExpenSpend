

using AutoMapper;
using ExpenSpend.Core.Friend;
using ExpenSpend.Repository.Friends;
using ExpenSpend.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenSpend.Web.Controllers.Friends
{
    [Route("api/friendship")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;

        public FriendshipController(IUserRepository userRepository, IFriendRepository friendRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _mapper = mapper;
        }

        [HttpGet("get-friendships")]
        public async Task<ActionResult> GetFriendshipsAsync()
        {
            var currUser = await _userRepository.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var friendships = await _friendRepository.GetFriendshipsAsync(currUser.Id);

            if (friendships != null)
            {
                return Ok(_mapper.Map<List<GetFriendshipDto>>(friendships));
            }
            else
            {
                return BadRequest("Friendships not found");
            }
        }

        [HttpGet("get-friendship-requests")]
        public async Task<IActionResult> GetFriendshipRequestsAsync()
        {
            var currUser = await _userRepository.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var friendshipRequests = await _friendRepository.GetFriendshipRequestsAsync(currUser.Id);
            if (friendshipRequests != null)
            {
                return Ok(_mapper.Map<List<GetFriendshipDto>>(friendshipRequests));
            }
            else
            {
                return BadRequest("Friendship requests not found");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateFriendshipDto friendshipDto)
        {
            var currUser = await _userRepository.GetUserByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var newFriendship = await _friendRepository.AddAsync(currUser.Id, friendshipDto.RecipientId);
            if (newFriendship != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Friendship already exists");
            }
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.AcceptAsync(friendshipId);
            if (friendship != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Friendship not found");
            }
        }

        [HttpPut("decline")]
        public async Task<IActionResult> DeclineAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.DeclineAsync(friendshipId);
            if (friendship != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Friendship not found");
            }
        }

        [HttpPut("block")]
        public async Task<IActionResult> BlockAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.BlockAsync(friendshipId);
            if (friendship != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Friendship not found");
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteAsync(Guid friendshipId)
        {
            var friendship = await _friendRepository.DeleteAsync(friendshipId);
            if (friendship != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Friendship not found");
            }
        }
    }
}
