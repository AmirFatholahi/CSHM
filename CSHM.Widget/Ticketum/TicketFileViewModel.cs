using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class TicketFileViewModel
    {
        public int ID { get; set; }
        public int TicketDetailID { get; set; }
        public int DocumentFileID { get; set; }
        public string StreamId { get; set; }
        public string orginalFileName { get; set; }
        public string ExtentionType { get; set; }
        public int Size { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsActive { get; set; }
    }
}
