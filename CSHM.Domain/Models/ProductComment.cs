using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class ProductComment:IEntity
    {
        public ProductComment() { }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public string Note { get; set; }

        public string NoteDate { get; set; }

        public string NoteTime { get; set; }

        public int Rate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
    }
}
