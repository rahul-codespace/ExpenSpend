using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Domain.DTOs.Expenses
{
    public class UpdateExpenseDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public bool IsSettled { get; set; }
    }
}
