using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentations.User
{
    public class UserAddressViewModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string ReceiverFullName { get; set; }

        public int GeoProvinceID { get; set; }

        public string ProvinceName { get; set; }

        public int GeoCityID { get; set; }

        public string CityName { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string Unit { get; set; }

        public string PostalCode { get; set; }

        public string Cellphone { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }
    }
}
