using CSHM.Domain.Interfaces;

namespace CSHM.Domain
{
    public class Media:IEntity
    {
        public Media() { }

        public int ID { get; set; }

        public int MediaTypeID { get; set; }

        public int ExtensionTypeID { get; set; }

        public string EntityName { get; set; }

        public int EntityID { get; set; }

        public string FileName { get; set; }

        public string DisplayName { get; set; }

        public int Sequence { get; set; }

        public string ExtensionName { get; set; }

        public byte[] Data { get; set; }

        public long Size { get; set; }

        public bool IsDefault { get; set; }

        public bool IsConfirm { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual MediaType MediaType { get; set; }

        public virtual ExtensionType ExtensionType { get; set; }
    }
}
