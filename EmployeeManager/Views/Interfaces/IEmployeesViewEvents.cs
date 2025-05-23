using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Views.Interfaces
{
    public interface IEmployeesViewEvents
    {
        event EventHandler PositionFilterChanged;
        event EventHandler AddEmployeeClicked;
        event EventHandler DeleteEmployeeClicked;
        event EventHandler GenerateReportClicked;
    }

}
