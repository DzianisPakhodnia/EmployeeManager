using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Views.Interfaces
{
    public interface IEmployeesViewData
    {
        string SelectedPosition { get; }
        int? SelectedEmployeeId { get; }
        Employee NewEmployeeData { get; }
        void SetPositions(IEnumerable<string> positions);
        void ShowEmployees(IEnumerable<Employee> employees);
    }

}
