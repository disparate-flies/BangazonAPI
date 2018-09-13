using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }       
        public int AccountNo { get; set; }
        public string Type { get; set; }
        public string Nickname { get; set; }
        public Customer Customer { get; set; }
    }
}
