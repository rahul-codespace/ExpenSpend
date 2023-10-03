using ExpenSpend.Core.DTOs.GroupMembers;

namespace ExpenSpend.Core.DTOs.Groups
{
    public class GetGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public GetGroupMemberDto GroupMember { get; set; }
    }
}
