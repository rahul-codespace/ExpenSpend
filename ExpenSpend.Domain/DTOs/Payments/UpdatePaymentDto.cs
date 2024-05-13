using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Domain.DTOs.Payments
{
    public class UpdatePaymentDto
    {
        public double Amount { get; set; }
        public bool IsSettled { get; set; }
    }
}
