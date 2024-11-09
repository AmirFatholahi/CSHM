using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class ResponseTicketViewModel
    {
        public ResponseTicketViewModel() {}
        public int TicketID { get; set; }
        public int TicketDetailID { get; set; }
        public string TrackingCode { get; set; }
    }
}
