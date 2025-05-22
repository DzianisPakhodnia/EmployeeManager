using EmployeeManager.Models;
using EmployeeManager.Presenters;
using EmployeeManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManager.Views
{
    public partial class EmployeesForm : Form, IEmployeesView
    {
        private EmployeesPresenter presenter;
        public EmployeesForm()
        {
            InitializeComponent();
            presenter = new EmployeesPresenter(this, new EmployeesRepository("Data Source=DENPC;Initial Catalog=EmployeeManagerDb;Integrated Security=True;"));
        }

        public string SelectedPosition => throw new NotImplementedException();

        public int? SelectedEmployeeId => throw new NotImplementedException();

        public Employee NewEmployeeData => throw new NotImplementedException();

        public event EventHandler PositionFilterChanged;
        public event EventHandler AddEmployeeClicked;
        public event EventHandler DeleteEmployeeClicked;
        public event EventHandler GenerateReportClicked;

        public void SetPositions(IEnumerable<string> positions)
        {
            throw new NotImplementedException();
        }

        public void ShowEmployees(IEnumerable<Employee> employees)
        {
            dataGridViewEmployees.Rows.Clear();

            foreach (var emp in employees)
            {
                int rowIndex = dataGridViewEmployees.Rows.Add(
                    emp.Name,
                    emp.Surname,
                    emp.Position,
                    emp.BirthYear,
                    emp.Salary
                );
                dataGridViewEmployees.Rows[rowIndex].Tag = emp;
            }
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            PositionFilterChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
