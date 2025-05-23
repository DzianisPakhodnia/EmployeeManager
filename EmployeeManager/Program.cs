using EmployeeManager.Repositories;
using EmployeeManager.Services;
using EmployeeManager.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManager
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeDb"].ConnectionString;
            Application.Run(new EmployeesForm(new EmployeesRepository(connectionString), new ReportService()));
        }
    }
}
