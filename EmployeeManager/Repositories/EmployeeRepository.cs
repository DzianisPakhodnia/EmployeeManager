using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;


        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesByPosition(string position)
        {
            throw new NotImplementedException();
        }
    }
}
