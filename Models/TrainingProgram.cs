using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee EmployeeId { get; set; }
        public int TrainingId { get; set; }
        public Training TrainingId { get; set; }
    }
}
