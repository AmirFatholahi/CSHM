using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace CSHM.Widget.Excel;

public class ExcelWidget : IExcelWidget
{
    #region  Export
    public HttpResponseMessage GenerateExcel<T>(List<T> list, T footer, bool isLock = true, string fileName = "Report", TableStyles tableStyles = TableStyles.Medium2,
        string sheetName = "Sheet1", bool rightToLeft = true, bool autoFit = true)
    {

        var footerList = new List<T>();
        if (footer != null)
            footerList.Add(footer);
        List<ExcelParameterViewModel<T>> sheets = new List<ExcelParameterViewModel<T>>
        {
            new ExcelParameterViewModel<T>
            {
                List = list,
                IsLock = isLock,
                Footer = footerList,
                SheetName = sheetName,
                RightToLeft = rightToLeft,
                TableStyles = tableStyles,
            }
        };
        return GenerateExcel(sheets, fileName, autoFit);

    }
    public HttpResponseMessage GenerateExcel<TE, TH>(List<TE> excelSheets, List<TE> footer, List<TH> header, bool isLock = true, string fileName = "Report", TableStyles tableStyles = TableStyles.Medium2,
        string sheetName = "Sheet1", bool rightToLeft = true, bool autoFit = true)
    {

        List<ExcelParameterViewModel<TE, TH>> sheets = new List<ExcelParameterViewModel<TE, TH>>
        {
            new ExcelParameterViewModel<TE, TH>
            {
                List = excelSheets,
                IsLock = isLock,
                Footer = footer,
                SheetName = sheetName,
                RightToLeft = rightToLeft,
                TableStyles = tableStyles,
                Header = header
            }
        };
        return GenerateExcel(sheets, fileName, autoFit);
    }



    public HttpResponseMessage GenerateExcel<TE, TH>(List<TE> excelSheets, TE footer, List<TH> header, bool isLock = true, string fileName = "Report", TableStyles tableStyles = TableStyles.Medium2,
        string sheetName = "Sheet1", bool rightToLeft = true, bool autoFit = true)
    {
        var footerList = new List<TE>();
        if (footer != null)
            footerList.Add(footer);
        List<ExcelParameterViewModel<TE, TH>> sheets = new List<ExcelParameterViewModel<TE, TH>>
        {
            new ExcelParameterViewModel<TE, TH>
            {
                List = excelSheets,
                IsLock = isLock,
                Footer = footerList,
                SheetName = sheetName,
                RightToLeft = rightToLeft,
                TableStyles = tableStyles,
                Header = header
            }
        };
        return GenerateExcel(sheets, fileName, autoFit);
    }

    public HttpResponseMessage GenerateExcel<T>(List<ExcelParameterViewModel<T>> excelSheets,
        string fileName = "Report", bool autoFit = true)
    {
        List<ExcelParameterViewModel<T, T>> newExcelSheet = new List<ExcelParameterViewModel<T, T>>();
        newExcelSheet = excelSheets.Select(x => new ExcelParameterViewModel<T, T>
        {
            List = x.List,
            Footer = x.Footer,
            IsLock = x.IsLock,
            SheetName = x.SheetName,
            RightToLeft = x.RightToLeft,
            TableStyles = x.TableStyles,
            Header = null
        }).ToList();
        return GenerateExcel(newExcelSheet, fileName, autoFit);
    }
    public HttpResponseMessage GenerateExcel<T, TH>(List<ExcelParameterViewModel<T, TH>> excelSheets, string fileName = "Report", bool autoFit = true)
    {

        var sheetCount = 0;
        var pck = new ExcelPackage();
        //pck.Workbook.Properties.Title = "Iman";
        //Load a list of FileDTO objects from the datatable...
        foreach (var excelParameterViewModel in excelSheets)
        {

            sheetCount++;
            var propertyCount = excelParameterViewModel.List.Count == 0 ? 0 : PropertyCount(excelParameterViewModel.List[0]);

            if (string.IsNullOrWhiteSpace(excelParameterViewModel.SheetName))
                excelParameterViewModel.SheetName = $"Sheet {sheetCount}";
            var wsList = pck.Workbook.Worksheets.Add(excelParameterViewModel.SheetName);
            var autoHeader = true;
            var headerPropertyCount = excelParameterViewModel.Header == null || excelParameterViewModel.Header.Count == 0 ? 0 : PropertyCount(excelParameterViewModel.Header[0]);

            //Add Header Data
            if (excelParameterViewModel.Header != null)
            {
                autoHeader = false;
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].LoadFromCollection(excelParameterViewModel.Header, false);
                int rowIndex = 1;
                foreach (var header in excelParameterViewModel.Header)
                {
                    Type t = header.GetType();

                    PropertyInfo[] props = t.GetProperties();
                    int colIndex = 1;
                    var lastMergedIndex = 1;
                    foreach (var p in props)
                    {

                        if (string.IsNullOrEmpty(p.GetValue(header)?.ToString()) && colIndex != 1)
                        {
                            wsList.Cells[rowIndex, lastMergedIndex - 1, rowIndex, colIndex].Merge = true;
                        }
                        else
                        {
                            lastMergedIndex = colIndex + 1;
                        }

                        colIndex++;
                    }

                    rowIndex++;
                }


            }

            //Add Footer Data
            if (excelParameterViewModel.Footer != null && excelParameterViewModel.Footer.Any()) excelParameterViewModel.List.AddRange(excelParameterViewModel.Footer);




            ExcelRangeBase rng;
            //Load the list using a specified array of MemberInfo objects. Properties, fields and methods are supported.
            if (excelParameterViewModel.Header == null)
            {
                rng = wsList.Cells["A1"].LoadFromCollection(excelParameterViewModel.List,
                    autoHeader,
                    excelParameterViewModel.TableStyles);
            }
            else
            {
                rng = wsList.Cells[excelParameterViewModel.Header.Count + 1, 1, excelParameterViewModel.List.Count + excelParameterViewModel.Header.Count, propertyCount].LoadFromCollection(excelParameterViewModel.List,
                    autoHeader,
                    excelParameterViewModel.TableStyles);
            }
            wsList.Cells.Style.Font.Name = "Tahoma";
            wsList.Cells.Style.Font.Size = 10;
            wsList.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            wsList.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //wsList.Cells[1, 1, wsList.Dimension.End.Row, wsList.Dimension.End.Column].Style.Font.Name = "B Nazanin";
            int firstFooterRow = excelParameterViewModel.List.Count - (excelParameterViewModel.Footer?.Count ?? 0) + (excelParameterViewModel.Header?.Count ?? 1) + 1;
            int lastRow = excelParameterViewModel.List.Count + (excelParameterViewModel.Header?.Count ?? 1);
            //Style footer Data
            if (excelParameterViewModel.Footer != null && excelParameterViewModel.Footer.Any() && propertyCount > 0)
            {


                wsList.Cells[firstFooterRow, 1, lastRow, propertyCount].Style.Font.Bold = true;
                wsList.Cells[firstFooterRow, 1, lastRow, propertyCount].Style.Font.Name = "B Nazanin";
                wsList.Row(lastRow).Height = 14.25;

                wsList.Cells[firstFooterRow, 1, lastRow, propertyCount].Style.Font.Size = 12;
                // wsList.Cells[lastRow, 1, lastRow, propertyCount].Style.Font.Italic = true;
                wsList.Cells[firstFooterRow, 1, lastRow, propertyCount].Style.Font.Color.SetColor(Color.White);
                wsList.Cells[firstFooterRow, 1, lastRow, propertyCount].Style.Fill.PatternType = ExcelFillStyle.Solid;
                wsList.Cells[firstFooterRow, 1, lastRow, propertyCount].Style.Fill.BackgroundColor
                    .SetColor(Color.DarkBlue);

            }
            //Style Header data
            if (excelParameterViewModel.Header != null)
            {
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Font.Bold =
                    true;
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Font.Name =
                    "B Nazanin";


                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Font.Size = 12;
                // wsList.Cells[lastRow, 1, lastRow, propertyCount].Style.Font.Italic = true;
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Font.Color
                    .SetColor(Color.White);
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Fill.PatternType
                    = ExcelFillStyle.Solid;
                var color = Color.FromArgb(91, 155, 213);
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Fill.BackgroundColor.SetColor(color);
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Top.Color.SetColor(Color.White);
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Bottom.Color.SetColor(Color.White);
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Left.Color.SetColor(Color.White);
                wsList.Cells[1, 1, excelParameterViewModel.Header.Count, headerPropertyCount].Style.Border.Right.Color.SetColor(Color.White);
                //wsList.Cells[excelParameterViewModel.Header.Count, 1, 5, headerPropertyCount].AutoFilter = true;
            }



            if (autoFit == true)
            {
                wsList.Cells[wsList.Dimension.Address].AutoFitColumns();
            }
            else
            {
                wsList.DefaultColWidth = 150;
            }
            wsList.View.RightToLeft = excelParameterViewModel.RightToLeft;
            if (excelParameterViewModel.Header == null)
                wsList.Cells[1, 1, 1, 6].AutoFilter = false;
            ////Format Cols
            if (propertyCount > 0)
            {
                Format(ref wsList, excelParameterViewModel.List.First());
            }

            ////Lock File
            //Lock(ref wsList, excelParameterViewModel.List.First(), excelParameterViewModel.IsLock);
            //wsList.Protection.AllowAutoFilter = true;
            //wsList.Protection.AllowFormatColumns = true;
            //wsList.Protection.AllowFormatRows = true;
            //wsList.Protection.SetPassword("AccordFile");

        }

        var bytes = pck.GetAsByteArray();
        MemoryStream ms = new MemoryStream(bytes);

        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(ms.ToArray())
        };
        // result.Content = new StreamContent(ms);
        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName + ".xlsx"

        };

        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //  result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms.excel");
        result.Content.Headers.ContentLength = bytes.Length;

        return result;

    }

    public HttpResponseMessage GenerateExcel(DataTable dataTable, string fileName = "Report",
        TableStyles tableStyles = TableStyles.Medium2, string sheetName = "Sheet1", bool autoFit = true, bool rightToLeft = true)
    {
        var pck = new ExcelPackage();

        //Create a datatable with the directories and files from the root directory...


        if (string.IsNullOrWhiteSpace(sheetName))
            sheetName = "Sheet 1";
        var wsdataTable = pck.Workbook.Worksheets.Add(sheetName);

        wsdataTable.Cells.Style.Font.Name = "Tahoma";
        wsdataTable.Cells.Style.Font.Size = 10;
        wsdataTable.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
        wsdataTable.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //wsList.Cells[1, 1, wsList.Dimension.End.Row, wsList.Dimension.End.Column].Style.Font.Name = "B Nazanin";

        wsdataTable.Cells["A1"].LoadFromDataTable(dataTable, true, tableStyles);

        wsdataTable.Cells[wsdataTable.Dimension.Address].AutoFitColumns();
        wsdataTable.View.RightToLeft = rightToLeft;
        wsdataTable.Cells[1, 1, 1, 6].AutoFilter = false;
        if (autoFit == true)
        {
            wsdataTable.Cells[wsdataTable.Dimension.Address].AutoFitColumns();
        }
        else
        {
            wsdataTable.DefaultColWidth = 150;
        }

        var bytes = pck.GetAsByteArray();
        MemoryStream ms = new MemoryStream(bytes);
        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(ms.ToArray())
        };
        // result.Content = new StreamContent(ms);
        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName + ".xlsx"

        };

        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //  result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms.excel");
        result.Content.Headers.ContentLength = bytes.Length;

        return result;
    }
    #endregion

    #region Import

    public List<T> ReadFromExcel<T>(string filePath, string sheetName) where T : class, new()
    {

        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return ReadFromExcel<T>(fileStream, sheetName);
    }
    public List<T> ReadFromExcel<T>(IFormFile file, string sheetName) where T : class, new()
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        ms.Seek(0, SeekOrigin.Begin);
        return ReadFromExcel<T>(ms, sheetName);
    }

    public List<T> ReadFromExcel<T>(FileStream fileStream, string sheetName) where T : class, new()
    {
        var memoryStream = new MemoryStream();

        fileStream.Position = 0;
        fileStream.CopyTo(memoryStream);
        return ReadFromExcel<T>(memoryStream, sheetName);
    }

    public List<T> ReadFromExcel<T>(MemoryStream stream, string sheetName) where T : class, new()
    {
        List<T> items = new List<T>();
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[sheetName];
        var displayNames = GetDisplayNames<T>(new T());
        if (worksheet == null) worksheet = package.Workbook.Worksheets[0];
        if (worksheet != null)
        {
            var rowCount = worksheet.Dimension.Rows;
            var colCount = worksheet.Dimension.Columns;
            var headers = new List<string>();
            for (var col = 1; col <= colCount; col++)
            {
                headers.Add(worksheet.Cells[1, col].Value?.ToString());
            }

            for (var row = 2; row <= rowCount; row++)
            {
                var item = new T();
                for (var col = 1; col <= colCount; col++)
                {
                    var propertyInfo = typeof(T).GetProperty(headers[col - 1]);

                    if (propertyInfo == null)
                    {
                        var propName = displayNames[headers[col - 1]];
                        if (!string.IsNullOrWhiteSpace(propName)) propertyInfo = typeof(T).GetProperty(propName);
                    }
                    if (propertyInfo != null)
                    {
                        var valueAsString = worksheet.Cells[row, col].Value?.ToString();
                        if (valueAsString != null)
                        {
                            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                                if (underlyingType != null)
                                {
                                    object parsedValue = null;
                                    try
                                    {
                                        parsedValue = Convert.ChangeType(valueAsString, underlyingType);
                                    }
                                    catch
                                    {
                                        // ignored

                                    }

                                    propertyInfo.SetValue(item, parsedValue);
                                }
                            }
                            else
                            {
                                object parsedValue = null;
                                try
                                {
                                    parsedValue = Convert.ChangeType(valueAsString, propertyInfo.PropertyType);
                                }
                                catch
                                {
                                    // ignored
                                }

                                propertyInfo.SetValue(item, parsedValue);
                            }
                        }
                    }
                }
                items.Add(item);
            }
        }

        return items;
    }
    #endregion

    #region  private Methods
    /// <summary>
    /// تعداد پراپرتی های کلاس
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private int PropertyCount<T>(T entity)
    {
        Type t = entity.GetType();

        PropertyInfo[] props = t.GetProperties();

        int count = props.Length;
        return count;
    }

    /// <summary>
    /// قفل کردن فیلد ها
    /// </summary>
    /// <param name="worksheet">اکسل</param>
    /// <param name="entity">کلاس ارسال شده</param>
    /// <param name="isLock">مقدار پیش فرض</param>
    private void Lock<T>(ref ExcelWorksheet worksheet, T entity, bool isLock)
    {
        Type t = entity.GetType();

        PropertyInfo[] props = t.GetProperties();
        int col = 0;
        foreach (var propertyInfo in props)
        {
            col++;
            var isLockaAttributeData = propertyInfo.CustomAttributes.SingleOrDefault(x => x.AttributeType.Name == typeof(ExcelLockAttribute).Name);
            //در صورت وجود IsLock
            if (isLockaAttributeData != null)
            {
                worksheet.Cells[1, col, 1048576, col].Style.Locked = (bool)isLockaAttributeData.ConstructorArguments.First().Value;
            }
            //در غیر این صورت مقدار پیش فرض
            else
            {
                worksheet.Cells[1, col, 1048576, col].Style.Locked = isLock;
            }
        }


    }

    private void Format<T>(ref ExcelWorksheet worksheet, T entity)
    {
        Type t = entity.GetType();

        PropertyInfo[] props = t.GetProperties();
        int col = 0;
        foreach (var propertyInfo in props)
        {
            col++;
            var excelFormatAttributeData = propertyInfo.CustomAttributes.SingleOrDefault(x => x.AttributeType.Name == typeof(ExcelFormatAttribute).Name);
            //در صورت وجود ExcelFormatAttribute
            if (excelFormatAttributeData != null)
            {
                worksheet.Cells[1, col, 1048576, col].Style.Numberformat.Format = (string)excelFormatAttributeData.ConstructorArguments.First().Value;
            }

        }


    }

    private Dictionary<string, string> GetDisplayNames<T>(T entity)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        Type t = entity.GetType();

        PropertyInfo[] props = t.GetProperties();
        int col = 0;
        foreach (var propertyInfo in props)
        {
            col++;
            var displayAttributeData = propertyInfo.CustomAttributes.SingleOrDefault(x => x.AttributeType.Name == typeof(DisplayNameAttribute).Name);
            //در صورت وجود DisplayNameAttribute
            if (displayAttributeData != null)
            {
                result.Add((string)displayAttributeData.ConstructorArguments.First().Value, propertyInfo.Name);
            }

        }
        return result;

    }
    #endregion
}