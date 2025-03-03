using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class Page : IEntity
{
    public Page() { }

    public int ID { get; set; }

    public int? ParentID { get; set; }

    public int PageTypeID { get; set; }

    public string Title { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public string Icon { get; set; }

    public int Priority { get; set; }

    public bool IsMenu { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public virtual Page Parent { get; set; }

    public virtual PageType PageType { get; set; }

    public virtual ICollection<Page> Children { get; set; }

    public virtual ICollection<ControllerAction> ControllerActions { get; set; }

}