using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class TicketViewModel
    {
        public int ID { get; set; }
        public int DepartementID { get; set; }
        public string DepartementTitle { get; set; }
        public int SectionID { get; set; }
        public string JiraCode { get; set; }
        public string Description { get; set; }
        public string SectionTitle { get; set; }
        public int ScoreTypeID { get; set; }
        public string ScoreTypeTitle { get; set; }
        public int StatusTypeID { get; set; }
        public string StatusTypeTitle { get; set; }
        public int PriorityTypeID { get; set; }
        public string PriorityTypeTitle { get; set; }
        public int ChannelID { get; set; }
        public string ChannelTitle { get; set; }
        public int TicketTypeID { get; set; }
        public string TicketTypeTitle { get; set; }
        public string Subject { get; set; }
        public string FirstMessage { get; set; }
        public string EndDate { get; set; }
        public string TrackingCode { get; set; }
        public bool IsAttachment { get; set; }
        public bool IsFlagged { get; set; }
        public int FlagTypeID { get; set; }
        public string FlagColor { get; set; }
        public bool IsUnread { get; set; }
        public bool IsPinned { get; set; }
        public string? QrCode { get; set; }
        public int CreatorID { get; set; }
        public string CreatorFullName { get; set; }
        public string CreatorUserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsMerge { get; set; }
        public bool IsObservation { get; set; }
        public bool IsOpenModalScore { get; set; }
        public int UnObservationCount { get; set; }
        public string CreationDateTime { get; set; }
        public int TotalTicketCount { get; set; }
        public string EstimateDate { get; set; }
    }
}
