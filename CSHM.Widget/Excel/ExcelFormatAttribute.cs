namespace CSHM.Widget.Excel;

[AttributeUsage(AttributeTargets.All)]
public class ExcelFormatAttribute : Attribute
{
    public readonly string Format;

    public ExcelFormatAttribute(string format)
    {
        Format = format;
    }
}