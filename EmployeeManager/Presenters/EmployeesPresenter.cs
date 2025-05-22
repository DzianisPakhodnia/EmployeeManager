using EmployeeManager.Models;
using EmployeeManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Presenters
{
    public class EmployeesPresenter 
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeesView _employeesView;

        public EmployeesPresenter(IEmployeesView employeesView,IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeesView = employeesView;
            _employeesView.PositionFilterChanged += OnPositionFilterChanged;
            _employeesView.AddEmployeeClicked += OnAddEmployeeClicked;
            _employeesView.DeleteEmployeeClicked += OnDeleteEmployeeClicked;
        }

        public void Initialize()
        {
            LoadPositions();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            _employeesView.ShowEmployees(employees);
        }

        private void LoadPositions()
        {
            var positions = _employeeRepository.GetAllPositions().ToList();
            positions.Insert(0, string.Empty);
            _employeesView.SetPositions(positions);
        }

        private void OnPositionFilterChanged(object sender, EventArgs e)
        {
            string selectedPosition = _employeesView.SelectedPosition;

            if (string.IsNullOrWhiteSpace(selectedPosition))
            {
                LoadEmployees(); 
            }
            else
            {
                var employees = _employeeRepository.GetEmployeesByPosition(selectedPosition);
                _employeesView.ShowEmployees(employees);
            }
        }

        private void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            var employee = _employeesView.NewEmployeeData;

            if (string.IsNullOrWhiteSpace(employee.Name) ||
                string.IsNullOrWhiteSpace(employee.Surname) ||
                string.IsNullOrWhiteSpace(employee.Position) ||
                employee.BirthYear < 1900 || employee.Salary <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Некорректные данные для добавления сотрудника.");
                return;
            }

            _employeeRepository.AddEmployee(employee);

            LoadEmployees();
            LoadPositions();
        }


        private void OnDeleteEmployeeClicked(object sender, EventArgs e)
        {
            var selectedId = _employeesView.SelectedEmployeeId;
            if (selectedId == null)
            {
                System.Windows.Forms.MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
                return;
            }

            _employeeRepository.DeleteEmployee(selectedId.Value);
            LoadEmployees();
            LoadPositions();

        }

    }
}
