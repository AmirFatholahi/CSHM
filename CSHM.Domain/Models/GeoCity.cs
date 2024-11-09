using CSHM.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Domain
{
    public class GeoCity:IEntity
    {
        public GeoCity() { }

        public int ID { get; set; }

        public int GeoProvinceID { get; set; }

        public string CityName { get; set; }

        public string CityCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorID { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int? ModifierID { get; set; }

        public DateTime? ModificationDateTime { get; set; }

        public virtual GeoProvince GeoProvince { get; set; }
    }
}
