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


        private Employee ReadEmployee(SqlDataReader reader)
        {
            return new Employee
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2),
                Position = reader.GetString(3),
                BirthYear = reader.GetInt32(4),
                Salary = reader.GetDecimal(5)
            };
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


        public void DeleteEmployee(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Employees WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
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
                            var employee = ReadEmployee(reader);
                            employees.Add(employee);
                        }

                    }
                }
            }

            return employees;
        }
        public IEnumerable<string> GetAllPositions()
        {
            var positions = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = "SELECT DISTINCT Position FROM Employees";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            positions.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return positions;
        }

        public IEnumerable<(string Position, decimal AverageSalary)> GetAverageSalaryByPosition()
        {
            var result = new List<(string Position, decimal AverageSalary)>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                string query = @"
                        SELECT Position, AVG(Salary) AS AverageSalary
                        FROM Employees
                        GROUP BY Position";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string position = reader.GetString(0);
                            decimal averageSalary = reader.GetDecimal(1);

                            result.Add((position, averageSalary));
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<Employee> GetEmployeesByPosition(string position)
        {
            var employees = new List<Employee>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var query = "SELECT Id, Name, Surname, Position, BirthYear, Salary FROM Employees WHERE Position = @Position";

                using (var command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Position", position);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = ReadEmployee(reader);
                            employees.Add(employee);
                        }

                    }
                }
            }

            return employees;

        }

    }
}
