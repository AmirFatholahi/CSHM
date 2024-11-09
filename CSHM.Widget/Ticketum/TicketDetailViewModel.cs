using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class TicketDetailViewModel
    {
        public TicketDetailViewModel() { }


        public int ID { get; set; }
        public int TicketID { get; set; }
        public string TicketSubject { get; set; }
        public int ParentID { get; set; }
        public int AssignTypeID { get; set; }
        public string? AssignTypeTitle { get; set; }
        public int SectionID { get; set; }
        public int FlagTypeID { get; set; }
        public string FlagColor { get; set; }
        public string? DepartementTitle { get; set; }
        public string? SectionTitle { get; set; }
        public int PriorityTypeID { get; set; }
        public string? PriorityTypeTitle { get; set; }
        public int TicketTypeID { get; set; }
        public string? TicketTypeTitle { get; set; }
        public int StatusTypeID { get; set; }
        public string StatusTypeTitle { get; set; }
        public string? Message { get; set; }
        public string CreationDateTime { get; set; }
        public string From { get; set; }
        public string FromFullName { get; set; }
        public int CreatorTicketID { get; set; }
        public string CreatorTicketTitle { get; set; }
        public List<string> To { get; set; }
        public List<string> ToFullName { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsUnread { get; set; }
        public bool IsPinned { get; set; }
        public bool IsActive { get; set; }
        public bool IsComment { get; set; }
        public bool IsObservation { get; set; }
        public string ObservationDateTime { get; set; }
        public string TrackingCode { get; set; }
        public string JiraCode { get; set; }

        public string Description { get; set; }

        public List<TicketFileViewModel> ticketFiles { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        // public List<TicketDetailViewModel>? Children { get; set; }

    }
}
