using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class Role : IdentityRole<int>, IEntity
{
    public Role() { }

    [NotMapped]
    public int ID { get => Id; set => Id = value; }

    public string Side { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; }

    public virtual ICollection<UserInRole> UserInRoles { get; set; }

}