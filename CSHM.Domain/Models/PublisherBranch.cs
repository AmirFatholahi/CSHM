using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class PublisherBranch:IEntity
    {
        public PublisherBranch() { }

        public int ID { get; set; }

        public int PublisherID { get; set; }

        public int GeoCityID { get; set; }

        public string Title { get; set; }

        public string Phone { get; set; }

        public string Cellphone { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual GeoCity GeoCity { get; set; }
    }
}
