using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class EmployeeComputer
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ComputerId { get; set; }
        public string DateAssigned { get; set; }
        public string DateTurnedIn { get; set; }
        public Employee Employee { get; set; }
        public Computer Computer { get; set; }
    }
}
