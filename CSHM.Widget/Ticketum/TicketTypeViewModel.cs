using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class TicketTypeViewModel
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public int? EstimateCounter { get; set; }
        public string? EstimateUnit { get; set; }
        public int SectionID { get; set; }
        public string SectionTitle { get; set; }
        public int SLAMetric1 { get; set; } //For First Response
        public int SLAMetric2 { get; set; } //For Closed Ticket
        public int SLAMetric3 { get; set; } //For 1 Responses Ticket
        public int SLAMetric4 { get; set; } //For 2 Responses Ticket
        public int SLAMetric5 { get; set; } //For 3 Responses Ticket
        public int SLAMetric6 { get; set; } //For 4 Responses Ticket
        public bool IsActive { get; set; }

    }
}
