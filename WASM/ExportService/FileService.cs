using BaseLibrary.Class.Entities;

namespace WASM.ExportService
{

        public class FileService
        {
            public byte[] ExportEmployeesToExcel(List<Employee> employees)
            {
                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employees");
                    worksheet.Cell(1, 1).Value = "Civil Id";
                    worksheet.Cell(1, 2).Value = "Name";
                    worksheet.Cell(1, 3).Value = "Job";
                    worksheet.Cell(1, 4).Value = "File No.";

                    for (int i = 0; i < employees.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = employees[i].CivilId;
                        worksheet.Cell(i + 2, 2).Value = employees[i].Name;
                        worksheet.Cell(i + 2, 3).Value = employees[i].JobName;
                        worksheet.Cell(i + 2, 4).Value = employees[i].FileNumber;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
        }

}
