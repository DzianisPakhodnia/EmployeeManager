using EmployeeManager.Models;
using EmployeeManager.Repositories;
using EmployeeManager.Services;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManager.Presenters
{
    public class EmployeesPresenter 
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeesView _employeesView;
        private readonly IReportService _reportService;

        public EmployeesPresenter(IEmployeesView employeesView,IEmployeeRepository employeeRepository, IReportService reportService)
        {
            _employeeRepository = employeeRepository;
            _employeesView = employeesView;
            _reportService = reportService;
            _employeesView.PositionFilterChanged += OnPositionFilterChanged;
            _employeesView.AddEmployeeClicked += OnAddEmployeeClicked;
            _employeesView.DeleteEmployeeClicked += OnDeleteEmployeeClicked;
            _employeesView.GenerateReportClicked += OnGenerateReportClicked;
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
                _employeesView.ShowError("Некорректные данные для добавления сотрудника.");
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
                _employeesView.ShowMessage("Пожалуйста, выберите сотрудника для удаления.");
                return;
            }

            var result = MessageBox.Show(
                "Вы действительно хотите удалить выбранного сотрудника?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _employeeRepository.DeleteEmployee(selectedId.Value);
                LoadEmployees();
                LoadPositions();
            }
        }

        private void OnGenerateReportClicked(object sender, EventArgs e)
        {
            try
            {
                var reportData = _employeeRepository.GetAverageSalaryByPosition();
                string filePath = _employeesView.AskSaveFilePath("Отчет_Средняя_Зарплата.xlsx");

                if (string.IsNullOrEmpty(filePath))
                    return;

                _reportService.GenerateAverageSalaryReport(filePath, reportData);

                _employeesView.ShowMessage($"Отчёт успешно сохранён:\n{filePath}");
            }
            catch (Exception ex)
            {
                _employeesView.ShowMessage($"Ошибка при создании отчёта:\n{ex.Message}");
            }
        }







    }
}
