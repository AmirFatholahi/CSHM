using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class FollowUpViewModel
    {
        public int TicketID { get; set; }
        public string ScoreTypeTitle { get; set; }
        public string StatusTypeTitle { get; set; }
        public string PriorityTypeTitle { get; set; }
        public string ChannelTitle { get; set; }
        public string TicketTypeTitle { get; set; }
        public string Subject { get; set; }
        public DateTime EndDate { get; set; }
        public string TrackingCode { get; set; }
        public string CreatorFullName { get; set; }
        public string OperatorDoingTaskFullName { get; set; }
        public bool IsActive { get; set; }
    }
}
