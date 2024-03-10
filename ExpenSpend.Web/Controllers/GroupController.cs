using ExpenSpend.Domain.DTOs.Groups;
using ExpenSpend.Domain.Models.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenSpend.Web.Controllers
{
    [ApiController]
    [Route("api/group")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupAppService _groupAppService;

        public GroupController(IGroupAppService groupAppService)
        {
            _groupAppService = groupAppService;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupAppService.GetAllGroupsAsync();
            return Ok(groups);
        }
        [HttpGet("group/{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _groupAppService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return Ok(group);
            }
            return NotFound();
        }
        [HttpGet("groups/{userId}")]
        public async Task<IActionResult> GetGroupsByUserId(Guid userId)
        {
            var groups = await _groupAppService.GetGroupsByUserId(userId);
            if(groups.StatusCode == 200)
            {
                return Ok(groups.Data);
            }
            return NotFound(groups.Message);
        }
        [HttpPost("group")]
        public async Task<IActionResult> CreateGroup(CreateGroupDto input)
        {
            var result = await _groupAppService.CreateGroupAsync(input);
            if (result.StatusCode == 201)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("group-with-members")]
        public async Task<IActionResult> CreateGroupWithMembers(CreateGroupWithMembersDto input)
        {
            var result = await _groupAppService.CreateGroupWithMembers(input);
            if (result.StatusCode == 201)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("group/{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, UpdateGroupDto input)
        {

            var result = await _groupAppService.UpdateGroupAsync(id , input);
            if (result.StatusCode == 200)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpDelete("group/{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var result = await _groupAppService.SoftDeleteAsync(id);
            if(result.StatusCode == 200)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }
    }
}
