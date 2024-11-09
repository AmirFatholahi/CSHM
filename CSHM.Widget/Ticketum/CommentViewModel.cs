using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class CommentViewModel
    {
        public int ID { get; set; }

        public string Message { get; set; }

        public string Content { get; set; }

        public int TicketDetailID { get; set; }

        public int StatusTypeID { get; set; }

        public string? Username { get; set; }

        public int DepartementID { get; set; }

        public string CreatorName { get; set; }

    }
}
