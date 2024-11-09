using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class TicketUserViewModel
    {
        public string? NID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int UserTypeID { get; set; }

        public int GroupID { get; set; }

        public string PhoneNumber { get; set; }

        public string AliasName { get; set; }

        public string FullName { get; set; }

        public string AliasUserName { get; set; }
        public int EffectiveRatio { get; set; }
        public bool IsReceivedEmail { get; set; }
        public bool IsReceivedSMS { get; set; }

        public IEnumerable<int>? Roles { get; set; }

    }
}
