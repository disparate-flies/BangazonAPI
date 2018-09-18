﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Title { get; set;}
        public string ProdDesc { get; set; }
        public int Quantity { get; set; }
        public int SellerId { get; set; }
        public Customer Customer { get; set;}
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
