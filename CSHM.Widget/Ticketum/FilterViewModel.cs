using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class FilterViewModel
    {
        public int? CreatorID { get; set; }
        public int? DepartementID { get; set; }
        public int? SectionID { get; set; }
        public int? TicketTypeID { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public int? ScoreTypeID { get; set; }
        public int? StatusTypeID { get; set; }
        public int? PriorityTypeID { get; set; }
        public int? ChannelID { get; set; }
        public string? TrackingCode { get; set; }
        public bool? IsFlagged { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public int? ResponceUser { get; set; }
        public int? RefferalUser { get; set; }
        public int? EndUser { get; set; }
        public bool IsCreatorSearch { get; set; }
    }
}
