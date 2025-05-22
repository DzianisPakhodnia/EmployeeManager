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
        public EmployeesForm(IEmployeeRepository repository)
        {
            InitializeComponent();
            presenter = new EmployeesPresenter(this, repository);
        }


        public string SelectedPosition => comboBoxPositions.SelectedItem?.ToString();

        public int? SelectedEmployeeId
        {
            get
            {
                if (dataGridViewEmployees.SelectedRows.Count > 0)
                {
                    var employee = dataGridViewEmployees.SelectedRows[0].Tag as Employee;
                    return employee?.Id;
                }
                return null;
            }
        }


        public Employee NewEmployeeData
        {
            get
            {
                return new Employee
                {
                    Name = textBoxName.Text.Trim(),
                    Surname = textBoxSurname.Text.Trim(),
                    Position = textBoxPosition.Text.Trim(),
                    BirthYear = int.TryParse(textBoxBirthYear.Text, out int birthYear) ? birthYear : 0,
                    Salary = decimal.TryParse(textBoxSalary.Text, out decimal salary) ? salary : 0
                };
            }
        }


        public event EventHandler PositionFilterChanged;
        public event EventHandler AddEmployeeClicked;
        public event EventHandler DeleteEmployeeClicked;
        public event EventHandler GenerateReportClicked;



        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            presenter.Initialize();

        }


        public void SetPositions(IEnumerable<string> positions)
        {
            comboBoxPositions.Items.Clear();
            comboBoxPositions.Items.AddRange(positions.ToArray());
            if (comboBoxPositions.Items.Count > 0)
                comboBoxPositions.SelectedIndex = 0;
        }


        public void ShowEmployees(IEnumerable<Employee> employees)
        {
            dataGridViewEmployees.Rows.Clear();

            foreach (var emp in employees)
            {
                int rowIndex = dataGridViewEmployees.Rows.Add(
                    emp.Id,
                    emp.Name,
                    emp.Surname,
                    emp.Position,
                    emp.BirthYear,
                    emp.Salary
                );
                dataGridViewEmployees.Rows[rowIndex].Tag = emp;
            }
        }

        

        private void buttonAddEmployee_Click(object sender, EventArgs e)
        {
            AddEmployeeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void comboBoxPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            PositionFilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void buttonDeleteEmployee_Click(object sender, EventArgs e)
        {
            DeleteEmployeeClicked?.Invoke(this, EventArgs.Empty);
        }

    }
}
