using CSHM.Domain.Interfaces;

namespace CSHM.Domain
{
    public class GenreType : IEntity
    {
        public GenreType()
        {
        }
        public int ID { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }
    }
}
