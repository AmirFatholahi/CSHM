using CSHM.Core.Handlers.Interfaces;


namespace CSHM.Core.Presentations.User;

public class UserViewModel : IDataRepository
{
    public int ID { get; set; }

    public string PersonCode { get; set; }

    public int UserTypeID { get; set; }

    public string UserTypeTitle { get; set; }

    public int ApprovementTypeID { get; set; }

    public string ApprovementTypeTitle { get; set; }

    public int GenderTypeID { get; set; }

    public string GenderTypeTitle { get; set; }

    public int GeoProvinceID { get; set; }

    public string GeoProvinceTitle { get; set; }

    public int GeoCityID { get; set; }

    public string GeoCityTitle { get; set; }

    public string NID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public string AliasName { get; set; }

    public string FatherName { get; set; }

    public string RegDate { get; set; }

    public string RegNumber { get; set; }

    public int DeadState { get; set; }

    public string UserName { get; set; }

    public string Cellphone { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string Avatar { get; set; }

    public bool IsBlueTick { get; set; }

    public bool IsForced { get; set; }

    public bool IsActive { get; set; }

    public string CaptchaWord { get; set; }

    public string CaptchaSessionID { get; set; }

    public string RepositoryID { get; set; }//شناسه ریپوزیتوری

    public DateTime? ExpireTime { get; set; }//زمان انقضا از ریپوزیتوری

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}