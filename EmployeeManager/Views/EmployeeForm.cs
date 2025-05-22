using EmployeeManager.Models;
using EmployeeManager.Presenters;
using EmployeeManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EmployeeManager.Views
{
    public partial class EmployeesForm : Form, IEmployeesView
    {
        private EmployeesPresenter presenter;
        public event EventHandler PositionFilterChanged;
        public event EventHandler AddEmployeeClicked;
        public event EventHandler DeleteEmployeeClicked;
        public event EventHandler GenerateReportClicked;
        public EmployeesForm(IEmployeeRepository repository, IReportService reportService)
        {
            InitializeComponent();

            presenter = new EmployeesPresenter(this, repository, reportService);
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

        private void buttonGenerateExcelReport_Click(object sender, EventArgs e)
        {
            GenerateReportClicked?.Invoke(this, EventArgs.Empty);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string GetSaveFilePath()
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel Files|*.xlsx";
                dlg.Title = "Сохранить отчет";
                dlg.FileName = "Средняя_зарплата.xlsx";

                if (dlg.ShowDialog() == DialogResult.OK)
                    return dlg.FileName;
                else
                    return null;
            }
        }

        public string AskSaveFilePath(string defaultFileName)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.FileName = defaultFileName;
                sfd.Filter = "Excel Files|*.xlsx|All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return sfd.FileName;
                }
                else
                {
                    return null; // Пользователь отменил
                }
            }
        }

    }
}
