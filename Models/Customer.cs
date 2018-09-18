//Created by Natasha Cox, 

using System.Collections.Generic;
using System;

namespace DFBangazon.Models
{
    public class Customer
{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AccountCreated { get; set; }
        public DateTime LastLogin { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
}
}
