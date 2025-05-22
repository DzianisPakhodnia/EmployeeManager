using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetEmployeesByPosition(string position);
    }
}
