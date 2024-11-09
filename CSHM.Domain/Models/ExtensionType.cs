using CSHM.Domain.Interfaces;

namespace CSHM.Domain
{
    public class ExtensionType:IEntity
    {
        public ExtensionType() { }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Postfix { get; set; }

        public string? MatcherType { get; set; }

        public byte[]? Matcher { get; set; }

        public bool IsImage { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }
    }
}
