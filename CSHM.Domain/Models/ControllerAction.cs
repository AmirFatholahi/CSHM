using CSHM.Domain.Interfaces;

namespace CSHM.Domain;

public class ControllerAction : IEntity
{
    public ControllerAction() { }

    public int ID { get; set; }

    public int Code { get; set; }

    public int PageID { get; set; }

    public string TitleFa { get; set; }

    public string TitleEn { get; set; }

    public string ControllerName { get; set; }

    public string ActionName { get; set; }

    public int Priority { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorID { get; set; }

    public DateTime CreationDateTime { get; set; }

    public int? ModifierID { get; set; }

    public DateTime? ModificationDateTime { get; set; }


    public virtual Page Page { get; set; }

}