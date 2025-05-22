using EmployeeManager.Models;
using EmployeeManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Presenters
{
    public class EmployeesPresenter : IEmployeesPresenter
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeesView _employeesView;

        public EmployeesPresenter(IEmployeesView employeesView,IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeesView = employeesView;

            _employeesView.PositionFilterChanged += OnPositionFilterChanged;
        }

        private void LoadEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            _employeesView.ShowEmployees(employees);
        }

        private void LoadPositions()
        {
            var positions = _employeeRepository.GetAllPositions();
            _employeesView.SetPositions(positions);
        }

        private void OnPositionFilterChanged(object sender, EventArgs e)
        {
            LoadEmployees();
        }




    }
}
