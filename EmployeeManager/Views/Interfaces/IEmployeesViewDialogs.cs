using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Views.Interfaces
{
    public interface IEmployeesViewDialogs
    {
        string GetSaveFilePath();
        string AskSaveFilePath(string defaultFileName);
        void ShowMessage(string message);
        void ShowError(string message);
    }

}
