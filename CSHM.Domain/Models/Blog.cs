using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class Blog:IEntity
    {
        public Blog() { }

        public int ID { get; set; }

        public int BlogTypeID { get; set; }

        public int BlogStatusTypeID { get; set; }

        public int PublisherID { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string MetaDescription { get; set; }

        public string CreationDate { get; set; }

        public string CreationTime { get; set; }

        public int VisitedCount { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual BlogType BlogType { get; set; }

        public virtual BlogStatusType BlogStatusType { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
