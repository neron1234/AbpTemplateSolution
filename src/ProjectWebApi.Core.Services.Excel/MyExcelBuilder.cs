using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectWebApi.Core.Services.Excel
{
    public class MyExcelBuilder : BaseExcelBuilder
    {
        private const string RESOURCE_FILE_PATH = "ProjectWebApi.Core.Services.Excel.Templates.MyExcelTemplate.xlsx";

        public MemoryStream CreateFile()
        {
            var outputStream = new MemoryStream();

            using (var templateStream = CopyResourceFile(RESOURCE_FILE_PATH))
            using (var file = new ExcelPackage(templateStream))
            {
                var sheet1 = file.Workbook.Worksheets[0];

                sheet1.Cells.AutoFitColumns();
                file.SaveAs(outputStream);
            }

            return outputStream;
        }
    }
}
