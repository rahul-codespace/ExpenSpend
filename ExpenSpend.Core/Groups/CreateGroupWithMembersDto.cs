using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Core.Groups
{
    public class CreateGroupWithMembersDto
    {
        public string Name { get; set; } = null!;
        public string? About { get; set; }
        public List<Guid> MemberIds { get; set; } = null!;
    }
}
