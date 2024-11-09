using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class Policy : IEntity
{
    public Policy() { }

    public int ID { get; set; }

    public int PolicyParameterID { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual PolicyParameter PolicyParameter { get; set; }

}