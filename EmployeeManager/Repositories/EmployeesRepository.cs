using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Repositories
{
    public class EmployeesRepository : IEmployeeRepository
    {
        private readonly string _connectionString;


        public EmployeesRepository(string connectionString)
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
            var employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Surname, Position, BirthYear, Salary FROM Employees";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                Position = reader.GetString(3),
                                BirthYear = reader.GetInt32(4),
                                Salary = reader.GetDecimal(5)
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
        public IEnumerable<string> GetAllPositions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeesByPosition(string position)
        {
            throw new NotImplementedException();
        }
    }
}
