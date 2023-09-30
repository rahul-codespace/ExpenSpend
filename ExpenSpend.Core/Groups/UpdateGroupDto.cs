using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Core.Groups
{
    public class UpdateGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? About { get; set; }
    }
}
