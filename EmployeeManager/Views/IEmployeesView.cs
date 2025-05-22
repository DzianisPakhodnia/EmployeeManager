using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Presenters
{
    public interface IEmployeesView
    {
        event EventHandler PositionFilterChanged;
        event EventHandler AddEmployeeClicked;
        event EventHandler DeleteEmployeeClicked;
        event EventHandler GenerateReportClicked;

        string SelectedPosition { get; }
        int? SelectedEmployeeId { get; }
        Employee NewEmployeeData { get; }
        void SetPositions(IEnumerable<string> positions);
        void ShowEmployees(IEnumerable<Employee> employees);

    }
}
