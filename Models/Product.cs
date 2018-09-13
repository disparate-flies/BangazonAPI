using System;
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
        public string ProductDesc { get; set; }
        public int Quantity { get; set; }
        public Customer CustomerId { get; set;}
        public ProductType ProductTypeId { get; set; }
    }
}
