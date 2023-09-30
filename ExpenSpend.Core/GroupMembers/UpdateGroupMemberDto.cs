using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Core.GroupMembers
{
    public class UpdateGroupMemberDto
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
