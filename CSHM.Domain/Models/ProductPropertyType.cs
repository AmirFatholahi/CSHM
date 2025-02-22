using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class ProductPropertyType : IEntity
{
    public ProductPropertyType()
    {
    }

    public int ID { get; set; }

    public int ProductID{ get; set; }

    public int PropertyTypeID { get; set; }

    public string Value { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual Product Product { get; set; }

    public virtual PropertyType PropertyType { get; set; }

}
