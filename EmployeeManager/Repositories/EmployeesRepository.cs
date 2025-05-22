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
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = @"
                               INSERT INTO Employees (Name, Surname, Position, BirthYear, Salary)
                               VALUES (@Name, @Surname, @Position, @BirthYear, @Salary)";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Surname", employee.Surname);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@BirthYear", employee.BirthYear);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
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
