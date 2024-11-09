namespace CSHM.Widget.Excel;

[AttributeUsage(AttributeTargets.All)]
public class ExcelLockAttribute : Attribute
{
    public readonly bool IsLock;

    public ExcelLockAttribute(bool isLock = true)
    {
        this.IsLock = isLock;
    }
}