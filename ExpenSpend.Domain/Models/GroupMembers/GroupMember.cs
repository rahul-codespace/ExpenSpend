using ExpenSpend.Domain.Models.Groups;
using ExpenSpend.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Domain.Models.GroupMembers
{
    public class GroupMember : BaseEntity
    {
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public Guid UserId { get; set; }
        public ESUser? User { get; set; }
        public bool IsAdmin { get; set; }
    }
}
