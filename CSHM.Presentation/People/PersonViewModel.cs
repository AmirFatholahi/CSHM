using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentation.People
{
    public class PersonViewModel
    {
        public int ID { get; set; }

        public int GenderTypeID { get; set; }

        public string GenderTypeTitle { get; set; }

        public int GeoCountryID { get; set; }

        public string GeoCountryTitle { get; set; }

        public string FirstName { get; set; }

        public string FullName { get; set; }

        public string AliasName { get; set; }

        public string BirthDate { get; set; }

        public string Biography { get; set; }

        public bool IsActive { get; set; }
    }
}
