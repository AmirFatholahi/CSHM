using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class RequestTicketViewModel
    {
        public RequestTicketViewModel(){}

        public int StatusTypeID { get; set; }

        public int? TicketID { get; set; }

        public int? ParentID { get; set; }

        public List<int>? UsersID { get; set; }

        public int PriorityTypeID { get; set; }

        public int SectionID { get; set; }

        public int ChannelID { get; set; }

        public int TicketTypeID { get; set; }

        public int AssignTypeID { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Content { get; set; }

        public bool IsNeedToCompress { get; set; }

        public bool IsNeedToGrayscale { get; set; }

        public string? Username { get; set; }

        public ICollection<IFormFile>? files { get; set; }
    }
}
