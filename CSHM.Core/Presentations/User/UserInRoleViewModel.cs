namespace CSHM.Core.Presentations.User;

public class UserInRoleViewModel
{
    public int ID { get; set; }


    public int RoleID { get; set; }

    public int UserID { get; set; }

    public string RoleName { get; set; }

    public string Side { get; set; }

    public string ExpirationDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool IsActive { get; set; }

}