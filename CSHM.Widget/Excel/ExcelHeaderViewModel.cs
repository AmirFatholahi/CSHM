namespace CSHM.Widget.Excel;

public class ExcelHeaderViewModel
{
    public int ReportID { get; set; }
    public string ReportTitle { get; set; }
    public string Title { get; set; }
    public string DisplayName { get; set; }
    public string ExcelFormat { get; set; }
    public bool? IsLock { get; set; }
    public bool IsActive { get; set; }
}