using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class UserInRole : IdentityUserRole<int>, IEntity
{
    public UserInRole() { }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual User User { get; set; }

    public virtual Role Role { get; set; }



}