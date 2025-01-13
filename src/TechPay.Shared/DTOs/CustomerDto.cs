using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TechPay.Shared.DTOs
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
