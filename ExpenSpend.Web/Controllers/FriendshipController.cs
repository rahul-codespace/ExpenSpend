using ExpenSpend.Domain.DTOs.Friends;
using ExpenSpend.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenSpend.Web.Controllers
{
    [Route("api/friendships")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendAppService _friendService;

        public FriendshipController(IFriendAppService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFriendships()
        {
            var friendships = await _friendService.GetFriendshipsAsync();
            if (friendships.IsSuccess)
            {
                return Ok(friendships.Data);
            }
            return NotFound(friendships.Data);
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetFriendshipRequests()
        {
            var friendshipRequests = await _friendService.GetFriendshipRequestsAsync();
            if (friendshipRequests.IsSuccess)
            {
                return Ok(friendshipRequests.Data);
            }
            return NotFound(friendshipRequests.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriendship(CreateFriendshipDto friendshipDto)
        {
            var newFriendship = await _friendService.CreateFriendAsync(friendshipDto.RecipientId);
            if (newFriendship.IsSuccess)
            {
                return Ok(newFriendship.Data);
            }
            return BadRequest(newFriendship.Data);
        }

        [HttpPut("accept/{id}")]
        public async Task<IActionResult> AcceptFriendship(Guid id)
        {
            var friendship = await _friendService.AcceptAsync(id);
            if (friendship.IsSuccess)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Data);
        }

        [HttpPut("decline/{id}")]
        public async Task<IActionResult> DeclineFriendship(Guid id)
        {
            var friendship = await _friendService.DeclineAsync(id);
            if (friendship.IsSuccess)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Data);
        }

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockFriendship(Guid id)
        {
            var friendship = await _friendService.BlockAsync(id);
            if (friendship.IsSuccess)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendship(Guid id)
        {
            var friendship = await _friendService.SoftDeleteFriendAsync(id);
            if (friendship.IsSuccess)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Data);
        }
    }
}
