namespace CSHM.Core.Presentations.User;

public class UserExcelModel
{
    public int ID { get; set; }

    public int UserTypeID { get; set; }

    public int PersonTypeID { get; set; }

    public int GenderTypeID { get; set; }

    public string PersonTypeTitle { get; set; }

    public string GenderTypeTitle { get; set; }

    public string NID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public string FatherName { get; set; }

    public string RegDate { get; set; }

    public string RegNumber { get; set; }

    public string UserName { get; set; }

    public string Cellphone { get; set; }

    public string Phone { get; set; }

    public string Longitude { get; set; }

    public string Latitude { get; set; }

    public string Email { get; set; }

    public string PostalCode { get; set; }

    public string Address { get; set; }

    public byte[]? Avatar { get; set; }

    public string SecretKey { get; set; }

    public bool IsForced { get; set; }

    public bool IsActive { get; set; }
}