using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class User : IdentityUser<int>, IEntity
{
    public User() { }

    [NotMapped]
    public int ID { get => Id; set => Id = value; }

    public int UserTypeID { get; set; }

    public int PersonTypeID { get; set; }

    public int GenderTypeID { get; set; }

    public int GeoCityID { get; set; }

    public string NID { get; set; }

    public string FirstName { get; set; }

    public string FullName { get; set; }

    public string AliasName { get; set; }

    public string RegDate { get; set; }

    public string RegNumber { get; set; }
  
    public override string UserName { get; set; }

    public string Phone { get; set; }

    public string Cellphone { get; set; }

    public string PostalCode { get; set; }

    public string Address { get; set; }

    public string SecretKey { get; set; }

    public string RegistrationDate { get; set; } //تاریخ ایجاد شمسی

    public string Longitude { get; set; }

    public string Latitude { get; set; }

    public bool IsGaurd { get; set; }

    public bool IsForced { get; set; }

    public bool HasTicketum { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual UserType UserType { get; set; }

    public virtual PersonType PersonType { get; set; }

    public virtual GenderType GenderType { get; set; }

    public virtual GeoCity GeoCity { get; set; }



    public virtual ICollection<UserInRole> UserInRoles { get; set; }

    public virtual ICollection<UserPolicy> UserPolicies { get; set; }




  
}