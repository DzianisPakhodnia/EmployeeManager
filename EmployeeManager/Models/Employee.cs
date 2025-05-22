using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }      
        public int BirthYear { get; set; }        
        public decimal Salary { get; set; }

    }
}
