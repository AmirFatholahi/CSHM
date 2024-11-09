using System.Data;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.Table;

namespace CSHM.Widget.Excel;

public interface IExcelWidget
{
    HttpResponseMessage GenerateExcel<T>(List<T> list, T footer, bool isLock = true,
        string fileName = "Report", TableStyles tableStyles = TableStyles.Medium2,
        string sheetName = "Sheet1", bool rightToLeft = true, bool autoFit = true);

    HttpResponseMessage GenerateExcel<TE, TH>(List<TE> excelSheets, List<TE> footer, List<TH> header,
        bool isLock = true, string fileName = "Report", TableStyles tableStyles = TableStyles.Medium2,
        string sheetName = "Sheet1", bool rightToLeft = true, bool autoFit = true);

    HttpResponseMessage GenerateExcel<TE, TH>(List<TE> excelSheets, TE footer, List<TH> header, bool isLock = true,
        string fileName = "Report", TableStyles tableStyles = TableStyles.Medium2,
        string sheetName = "Sheet1", bool rightToLeft = true, bool autoFit = true);

    HttpResponseMessage GenerateExcel<T>(List<ExcelParameterViewModel<T>> excelSheets,
        string fileName = "Report", bool autoFit = true);

    HttpResponseMessage GenerateExcel<T, TH>(List<ExcelParameterViewModel<T, TH>> excelSheets,
        string fileName = "Report", bool autoFit = true);

    HttpResponseMessage GenerateExcel(DataTable dataTable, string fileName = "Report",
        TableStyles tableStyles = TableStyles.Medium2, string sheetName = "Sheet1", bool autoFit = true,
        bool rightToLeft = true);

    List<T> ReadFromExcel<T>(string filePath, string sheetName) where T : class, new();
    List<T> ReadFromExcel<T>(IFormFile file, string sheetName) where T : class, new();
    
    List<T> ReadFromExcel<T>(FileStream stream, string sheetName) where T : class, new();
    List<T> ReadFromExcel<T>(MemoryStream stream, string sheetName) where T : class, new();
}