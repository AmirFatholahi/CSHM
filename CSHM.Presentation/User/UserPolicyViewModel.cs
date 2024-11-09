namespace CSHM.Presentations.User;

public class UserPolicyViewModel
{
    public int ID { get; set; }

    public int PolicyParameterID { get; set; }

    public string PolicyParameterTitle { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public int PolicyID { get; set; }

    public int UserID { get; set; }

    public bool IsActive { get; set; }

}