using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class Computer
    {
        public int Id { get; set; }
        public string PurchaseDate { get; set; }
        public string Model { get; set; }
        public string DecommissionDate { get; set; }
        public string Condition { get; set; }
    }
}
