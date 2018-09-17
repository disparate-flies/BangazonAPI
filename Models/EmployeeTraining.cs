using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class EmployeeTraining
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TrainingId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public Employee Employee { get; set; }
    }
}
