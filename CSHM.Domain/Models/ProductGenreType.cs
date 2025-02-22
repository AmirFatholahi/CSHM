using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class ProductGenreType : IEntity
{
    public ProductGenreType()
    {
        
    }

    public int ID { get; set; }

    public int ProductID { get; set; }

    public int GenreTypeID { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual Product Product { get; set; }

    public virtual GenreType GenreType { get; set; }
}
