using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class UserPolicy : IEntity
{
    public UserPolicy() { }

    public int ID { get; set; }

    public int PolicyID { get; set; }

    public int UserID { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual Policy Policy { get; set; }

    public virtual User User { get; set; }



}