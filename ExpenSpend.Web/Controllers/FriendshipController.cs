using ExpenSpend.Core.DTOs.Friends;
using ExpenSpend.Domain.Models.Friends;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenSpend.Web.Controllers
{
    [Route("api/friendship")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendAppService _friendService;

        public FriendshipController(IFriendAppService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet("get-friendships")]
        public async Task<ActionResult> GetFriendshipsAsync()
        {
            var friendships = await _friendService.GetFriendshipsAsync();
            if(friendships.StatusCode == 200)
            {
                return Ok(friendships.Data);
            }
            return BadRequest(friendships.Message);
        }

        [HttpGet("get-friendship-requests")]
        public async Task<IActionResult> GetFriendshipRequestsAsync()
        {

            var friendshipRequests = await _friendService.GetFriendshipRequestsAsync();
            if(friendshipRequests.StatusCode == 200) {
                return Ok(friendshipRequests.Data);
            }
            return BadRequest(friendshipRequests.Message);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateFriendshipDto friendshipDto)
        {
            var newFriendship = await _friendService.CreateFriendAsync(friendshipDto.RecipientId);
            if (newFriendship.StatusCode == 201)
            {
                return StatusCode(201, newFriendship.Data);
            }
            else
            {
                return BadRequest(newFriendship.Message);
            }
        }

        [HttpPut("accept/{id}")]
        public async Task<IActionResult> AcceptAsync(Guid id)
        {
            var friendship = await _friendService.AcceptAsync(id);
            if(friendship.StatusCode == 200)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Message);
        }

        [HttpPut("decline/{id}")]
        public async Task<IActionResult> DeclineAsync(Guid id)
        {
            var friendship = await _friendService.DeclineAsync(id);
            if(friendship.StatusCode == 200)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Message);
        }

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockAsync(Guid id)
        {
            var friendship = await _friendService.BlockAsync(id);
            if(friendship.StatusCode == 200)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Message);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var friendship = await _friendService.SoftDeleteFriendAsync(id);
            if(friendship.StatusCode == 200)
            {
                return Ok(friendship.Data);
            }
            return NotFound(friendship.Message);
        }
    }
}
