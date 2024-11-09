using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentations.User
{
    public class UserActiveDirectoryInfoViewModel
    {
        public string Id { get; set; }

        public string PersonelId { get; set; }

        public string Domain { get; set; }

        public string ADId { get; set; }

        public string PersonelNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UnitId { get; set; }

        public string UnitName { get; set; }

        public string MehrCode { get; set; }

        public string UnitCode { get; set; }

        public string UnitTypeCode { get; set; }

        public string UnitTypeName { get; set; }

        public int StateCode { get; set; }

        public string StateCaption { get; set; }

        public string PostCode { get; set; }

        public string PostName { get; set; }

        public string JobCode { get; set; }

        public string JobName { get; set; }

        public string ProvinceCode { get; set; }

        public string ProvinceName { get; set; }

        public bool Enabled { get; set; }

        public bool Status { get; set; }

        public int ExpireDay { get; set; }
    }
}
