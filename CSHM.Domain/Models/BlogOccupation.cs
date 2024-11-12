using CSHM.Domain.Interfaces;

namespace CSHM.Domain
{
    public class BlogOccupation:IEntity
    {
        public BlogOccupation() { }

        public int ID { get; set; }

        public int BlogID { get; set; }

        public int PersonOccupationID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual PersonOccupation PersonOccupation { get; set; }

    }
}
