using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class RoleClaim : IEntity
{
    public RoleClaim() { }

    public int ID { get; set; }

    public int RoleID { get; set; }

    public int ControllerActionCode { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual Role Role { get; set; }

}