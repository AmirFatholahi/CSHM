namespace CSHM.Core.Presentations.User;

public class PolicyViewModel
{
    public int ID { get; set; }
    public int PolicyParameterID { get; set; }
    public string PolicyParameterTitle { get; set; }
    public string EntityType { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public bool IsActive { get; set; }

}