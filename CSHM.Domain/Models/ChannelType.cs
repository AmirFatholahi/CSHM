using CSHM.Domain.Interfaces;


namespace CSHM.Domain
{
    public class ChannelType:IEntity
    {
        public int ID { get; set; }

        public int TicketumSectionTypeID { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }
    }
}
