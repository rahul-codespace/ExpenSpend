using ExpenSpend.Core.DTOs.GroupMembers;
using ExpenSpend.Domain.Models.GroupMembers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExpenSpend.Web.Controllers
{
    [Route("api/group-member")]
    [ApiController]
    [Authorize]
    public class GroupMemberController : ControllerBase
    {
        private readonly IGroupMemberAppService _groupMemberService;

        public GroupMemberController(IGroupMemberAppService groupMemberService)
        {
            _groupMemberService = groupMemberService;
        }
        [HttpGet("group-members")]
        public async Task<IActionResult> GetAllGroupMembers()
        {
            var groupMembers = await _groupMemberService.GetAllGroupMembersAsync();
            if(groupMembers.StatusCode == 200)
            {
                return Ok(groupMembers.Data);
            }
            return NotFound(groupMembers.Message);
        }
        [HttpGet("group-member/{id}")]
        public async Task<IActionResult> GetGroupMemberById(Guid id)
        {
            var groupMember = await _groupMemberService.GetGroupMemberByIdAsync(id);
            if (groupMember.StatusCode == 200)
            {
                return Ok(groupMember.Data);
            }
            return NotFound(groupMember.Message);
        }

        [HttpPost("group-member")]
        public async Task<IActionResult> CreateGroupMember(CreateGroupMemberDto input)
        {
            var result = await _groupMemberService.CreateGroupMemberAsync(input);
            if (result.StatusCode == 201)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("make-group-admin/{id}")]
        public async Task<IActionResult> MakeGroupAdmin(Guid id)
        {
           var result = await _groupMemberService.MakeGroupAdminAsync(id);
            if(result.StatusCode == 200)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpPut("remove-group-admin/{id}")]
        public async Task<IActionResult> RemoveGroupAdmin(Guid id)
        {
            var result = await _groupMemberService.RemoveGroupAdminAsync(id);
            if (result.StatusCode == 200)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("remove-group-member/{id}")]
        public async Task<IActionResult> DeleteGroupMember(Guid id)
        {
            var result = await _groupMemberService.DeleteGroupMemberAsync(id);
            if (result.StatusCode == 200)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
