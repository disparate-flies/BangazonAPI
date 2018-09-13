using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public double ExpenseBudget { get; set; }
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

    }
}
