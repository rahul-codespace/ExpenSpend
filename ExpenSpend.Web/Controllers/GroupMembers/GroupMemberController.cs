using ExpenSpend.Core.GroupMembers;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Repository.GroupMembers;
using ExpenSpend.Repository.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers.GroupMembers
{
    [Route("api/group-member")]
    [ApiController]
    [Authorize]
    public class GroupMemberController : ControllerBase
    {
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IGroupRepository _groupRepository;
        
        public GroupMemberController(IGroupMemberRepository groupMemberRepository, IGroupRepository groupRepository)
        {
            _groupMemberRepository = groupMemberRepository;
            _groupRepository = groupRepository;
        }
        [HttpGet("group-members")]
        public async Task<IActionResult> GetAllGroupMembers()
        {
            var groupMembers = await _groupMemberRepository.GetAllGroupMembers();
            return Ok(groupMembers);
        }
        [HttpGet("group-member/{id}")]
        public async Task<IActionResult> GetGroupMemberById(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetGroupMemberById(id);
            if (groupMember != null)
            {
                return Ok(groupMember);
            }
            return NotFound();
        }
        [HttpGet("group-members/{groupId}")]
        public async Task<IActionResult> GetGroupMembersByGroupId(Guid groupId)
        {
            var groupMembers = await _groupMemberRepository.GetGroupMembersByGroupId(groupId);
            if (groupMembers != null)
            {
                return Ok(groupMembers);
            }
            return NotFound();
        }
        [HttpGet("group-members/{userId}")]
        public async Task<IActionResult> GetGroupMembersByUserId(Guid userId)
        {
            var groupMembers = await _groupMemberRepository.GetGroupMembersByUserId(userId);
            if (groupMembers != null)
            {
                return Ok(groupMembers);
            }
            return NotFound();
        }
        [HttpPost("group-member")]
        public async Task<IActionResult> CreateGroupMember(CreateGroupMemberDto input)
        {
            var group = await _groupRepository.GetGroupById(input.GroupId);
            if (group != null)
            {
                var newGroupMember = new GroupMember
                {
                    GroupId = input.GroupId,
                    UserId = input.UserId,
                };

                var result = await _groupMemberRepository.CreateGroupMember(newGroupMember);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Group member already exists");
            }
            return NotFound("Group not found!");
        }

        [HttpPut("make-group-admin/{id}")]
        public async Task<IActionResult> MakeGroupAdmin(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetGroupMemberById(id);
            if (groupMember != null)
            {
                groupMember.IsAdmin = true;
                await _groupMemberRepository.UpdateGroupMember(groupMember);
                return Ok(groupMember);
            }
            return NotFound("Group member not found");
        }
        [HttpPut("remove-group-admin/{id}")]
        public async Task<IActionResult> RemoveGroupAdmin(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetGroupMemberById(id);
            if (groupMember != null)
            {
                groupMember.IsAdmin = false;
                await _groupMemberRepository.UpdateGroupMember(groupMember);
                return Ok(groupMember);
            }
            return NotFound("Group member not found");
        }

        [HttpDelete("remove-group-member/{id}")]
        public async Task<IActionResult> DeleteGroupMember(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetGroupMemberById(id);
            if (groupMember != null)
            {
                await _groupMemberRepository.SoftDeleteGroupMember(groupMember);
                return Ok();
            }
            return NotFound("Group member not found");
        }
    }
}
