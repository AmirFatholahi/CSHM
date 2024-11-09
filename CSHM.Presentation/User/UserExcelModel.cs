using System.ComponentModel;

namespace CSHM.Presentations.User;

public class UserExcelModel
{
    [DisplayName("شناسه")]
    public int ID { get; set; }

    [DisplayName("شناسه ملی")]
    public string NID { get; set; }

    [DisplayName("نام و نام خانوادگی")]
    public string FullName { get; set; }

    [DisplayName("نام تجاری")]
    public string AliasName { get; set; }

    [DisplayName("نام پدر")]
    public string FatherName { get; set; }

    [DisplayName("تاریخ تولد / ثبت")]
    public string RegDate { get; set; }

    [DisplayName("شماره شناسنامه / ثبت")]
    public string RegNumber { get; set; }

    [DisplayName("نام کاربری")]
    public string UserName { get; set; }

    [DisplayName("نوع کاربری")]
    public string UserTypeTitle { get; set; }

    [DisplayName("نوع شخص")]
    public string PersonTypeTitle { get; set; }

    [DisplayName("جنسیت")]
    public string GenderTypeTitle { get; set; }

    [DisplayName("شماره همراه")]
    public string Cellphone { get; set; }

    [DisplayName("تلفن ثابت")]
    public string Phone { get; set; }

    [DisplayName("ایمیل")]
    public string Email { get; set; }

    [DisplayName("استان")]
    public string GeoProvinceTitle { get; set; }

    [DisplayName("شهرستان")]
    public string GeoCityTitle { get; set; }

    [DisplayName("کد پستی")]
    public string PostalCode { get; set; }

    [DisplayName("آدرس")]
    public string Address { get; set; }

    [DisplayName("تاریخ ثبت نام")]
    public string RegistrationDate { get; set; }

    [DisplayName("نهاد صادرکننده")]
    public string ExporterFullName { get; set; }

    [DisplayName("وضعیت")]
    public string IsActiveTitle { get; set; }


}






