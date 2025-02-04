using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Presentations.User
{
    public class UserInformationViewModel
    {
        public int ID { get; set; }

        public int UserTypeID { get; set; }

        public string UserTypeTitle { get; set; }

        public int DeadState { get; set; } // وضعیت زنده بودن فرد

        public string NID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string FatherName { get; set; }

        public string RegDate { get; set; }

        public string RegNumber { get; set; }

        public string UserName { get; set; }

        public string Cellphone { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public bool IsActive { get; set; }
    }
}
