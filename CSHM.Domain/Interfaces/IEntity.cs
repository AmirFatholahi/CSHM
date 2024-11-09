using System.ComponentModel.DataAnnotations;

namespace CSHM.Domain.Interfaces;

public interface IEntity
{
    [Key]
    [Required]
    int ID { get; set; }

    [Required]
    bool IsActive { get; set; }

    [Required]
    bool IsDeleted { get; set; }

    int CreatorID { get; set; }

    DateTime CreationDateTime { get; set; }

    int? ModifierID { get; set; }

    DateTime? ModificationDateTime { get; set; }

}