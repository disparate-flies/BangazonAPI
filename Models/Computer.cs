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
        public string Condition { get; set; }
        public string DecomissionDate { get; set; }
        List<Employee> EmployeeList = new List<Employee>();
    }
}
