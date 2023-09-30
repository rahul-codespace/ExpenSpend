using ExpenSpend.Core.Groups;
using ExpenSpend.Domain.Models.GroupMembers;
using ExpenSpend.Domain.Models.Groups;
using ExpenSpend.Repository.GroupMembers;
using ExpenSpend.Repository.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenSpend.Web.Controllers.Groups
{
    [ApiController]
    [Route("api/group")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMemberRepository _groupMemberRepository;

        public GroupController(IGroupRepository groupRepository, IGroupMemberRepository groupMemberRepository)
        {
            _groupRepository = groupRepository;
            _groupMemberRepository = groupMemberRepository;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupRepository.GetAllGroups();
            return Ok(groups);
        }
        [HttpGet("group{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _groupRepository.GetGroupById(id);
            if (group != null)
            {
                return Ok(group);
            }
            return NotFound();
        }
        [HttpGet("groups/{userId}")]
        public async Task<IActionResult> GetGroupsByUserId(Guid userId)
        {
            var groups = await _groupRepository.GetGroupsByUserId(userId);
            if (groups != null)
            {
                return Ok(groups);
            }
            return NotFound();
        }
        [HttpGet("groups/{email}")]
        public async Task<IActionResult> GetGroupsByUserEmail(string email)
        {
            var groups = await _groupRepository.GetGroupsByUserEmail(email);
            if (groups != null)
            {
                return Ok(groups);
            }
            return NotFound();
        }
        [HttpPost("group")]
        public async Task<IActionResult> CreateGroup(CreateGroupDto input)
        {
            var newGroup = new Group { 
                Name = input.Name, 
                About = input.About,
                CreatedBy = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            };
            var group = await _groupRepository.CreateGroup(newGroup);
            if (group != null)
            {
                var newGroupMember = new GroupMember
                {
                    GroupId = group.Id,
                    UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    IsAdmin = true,
                };
                var groupMember = await _groupMemberRepository.CreateGroupMember(newGroupMember);
                if (groupMember != null)
                {
                    return Ok(group);
                }
                await _groupRepository.DeleteGroup(group);
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost("group-with-members")]
        public async Task<IActionResult> CreateGroupWithMembers(CreateGroupWithMembersDto input)
        {
            var newGroup = new Group
            {
                Name = input.Name,
                About = input.About,
                CreatedBy = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            };
            var group = await _groupRepository.CreateGroup(newGroup);
            if (group != null)
            {
                var newGroupMembers = new List<GroupMember>();
                foreach (var memberId in input.MemberIds)
                {
                    var newGroupMember = new GroupMember
                    {
                        GroupId = group.Id,
                        UserId = memberId
                    };
                    newGroupMembers.Add(newGroupMember);
                }
                var groupMembers = await _groupMemberRepository.CreateGroupMembers(newGroupMembers);
                return Ok(group);
            }
            return BadRequest();
        }
        [HttpPut("group")]
        public async Task<IActionResult> UpdateGroup(UpdateGroupDto input)
        {
            var group = await _groupRepository.GetGroupById(input.Id);
            if (group != null)
            {
                group.Name = input.Name;
                group.About = input.About;
                group.ModifiedBy = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                group.ModifiedAt = DateTime.Now;
                var updatedGroup = await _groupRepository.UpdateGroup(group);
                return Ok(updatedGroup);
            }
            return NotFound();
        }
        [HttpDelete("group/{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var group = await _groupRepository.GetGroupById(id);
            if (group != null)
            {
                await _groupRepository.SoftDeleteGroup(group);
                return Ok();
            }
            return NotFound();
        }
    }
}
