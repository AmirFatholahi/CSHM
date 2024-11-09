using System.ComponentModel.DataAnnotations.Schema;

namespace CSHM.Presentations.User;

public class UserViewModel 
{
    public int ID { get; set; }

    public int UserTypeID { get; set; }

    public string UserTypeTitle { get; set; }

    public int ExporterID { get; set; }

    public string ExporterFullName { get; set; }

    public string ExporterNID { get; set; }

    public int PersonTypeID { get; set; }

    public string PersonTypeTitle { get; set; }

    public int GenderTypeID { get; set; }

    public string GenderTypeTitle { get; set; }

    public int GeoProvinceID { get; set; }

    public string GeoProvinceTitle { get; set; }

    public int GeoCityID { get; set; }

    public string GeoCityTitle { get; set; }

    public int DeadState { get; set; } // وضعیت زنده بودن فرد

    public string NID { get; set; }

    public string FirstName { get; set; }

    public string FullName { get; set; }

    public string AliasName { get; set; }

    public string DisplayName { get; set; }

    public string FatherName { get; set; }


    public string RegDate { get; set; }

    public string RegNumber { get; set; }

    public string UserName { get; set; }

    public string Website { get; set; }

    public string Cellphone { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string PostalCode { get; set; }

    public string Address { get; set; }

    public string Avatar { get; set; }

    public string Longitude { get; set; }

    public string Latitude { get; set; }

    public string RegistrationDate { get; set; }

    public bool IsForced { get; set; }

    public bool IsActive { get; set; }

    public string IsActiveTitle { get; set; }

    public string CaptchaWord { get; set; }

    public string CaptchaSessionID { get; set; }

    public string RepositoryID { get; set; }//شناسه ریپوزیتوری

    public DateTime? ExpireTime { get; set; }//زمان انقضا از ریپوزیتوری

    public bool HasTicketum { get; set; }


}