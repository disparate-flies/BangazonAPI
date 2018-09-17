// Author: Sathvik Reddy
// Purpose: Model - a customer can have many Payment Types; however, if a cutomer wants to remove a Payment Type, then it will be removed from their view. Customer payment data remains in Bangazon's database, regardless of whether it is active or not.
// Fields: 6 fields are used in this field, including a primary key, a foreign key, 2 strings and 2 integers

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
        public string AccType { get; set; }
        public string Nickname { get; set; }
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
    }
}
