using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public class ReportService : IReportService
    {
        public void GenerateAverageSalaryReport(string filePath, IEnumerable<(string Position, decimal AverageSalary)> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Средняя зарплата");
                worksheet.Cell(1, 1).Value = "Должность";
                worksheet.Cell(1, 2).Value = "Средняя зарплата";

                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.Position;
                    worksheet.Cell(row, 2).Value = Math.Round(item.AverageSalary, 2);
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(filePath);
            }

        }
    }
}
