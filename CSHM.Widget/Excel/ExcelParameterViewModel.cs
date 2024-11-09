using OfficeOpenXml.Table;

namespace CSHM.Widget.Excel;

public class ExcelParameterViewModel<T, TH>
{
    public List<T> List { get; set; }
    public List<TH> Header { get; set; }
    public List<T> Footer { get; set; }

    public bool IsLock { get; set; } = true;
    public TableStyles TableStyles { get; set; } = TableStyles.Medium2;
    public string SheetName { get; set; }
    public bool RightToLeft { get; set; } = true;
}
public class ExcelParameterViewModel<T>
{
    public List<T> List { get; set; }
    public List<T> Footer { get; set; }

    public bool IsLock { get; set; } = true;
    public TableStyles TableStyles { get; set; } = TableStyles.Medium2;
    public string SheetName { get; set; }
    public bool RightToLeft { get; set; } = true;
}