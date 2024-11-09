using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class PersonOccupation:IEntity
    {
        public PersonOccupation() { }

        public int ID { get; set; }

        public int PersonID { get; set; }

        public int OccupationID { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual Person Person { get; set; }

        public virtual Occupation Occupation { get; set; }
    }
}
