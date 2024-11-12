using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain.Models
{
    public class ProductPublisher : IEntity
    {
        public ProductPublisher()
        {
            
        }

        public int ID { get; set; }

        public int ProductID { get; set; }

        public int PublisherID { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual Product Product { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
