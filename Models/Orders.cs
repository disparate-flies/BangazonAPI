﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class Orders
{
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public Customer Customer { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<Product> ProductList { get; set; } = new List<Product>();
}
}
