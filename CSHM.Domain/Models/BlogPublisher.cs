using CSHM.Domain.Interfaces;


namespace CSHM.Domain.Models
{
    public class BlogPublisher : IEntity
    {
        public BlogPublisher()        {                  }

        public int ID { get; set; }

        public int BlogID { get; set; }

        public int PublisherID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual Publisher Publisher { get; set; }

    }
}
