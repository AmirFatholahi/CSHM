namespace CSHM.Widget.Excel;

[AttributeUsage(AttributeTargets.All)]
public class ExcelColumnAttribute : System.Attribute
{
    public int ColumnIndex { get; set; }


    public ExcelColumnAttribute(int column)
    {
        ColumnIndex = column;
    }
}