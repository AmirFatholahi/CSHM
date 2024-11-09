using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class Person : IEntity
    {
        public Person() { }

        public int ID { get; set; }

        public int GenderTypeID { get; set; }

        public int GeoCountryID { get; set; }

        public string FirstName { get; set; }

        public string FullName { get; set; }

        public string AliasName { get; set; }

        public string BirthDate { get; set; }

        public string Biography { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual GenderType GenderType { get; set; }

        public virtual GeoCountry GeoCountry { get; set; }

        public virtual ICollection<PersonOccupation> PersonOccupations { get; set; }
    }
}
