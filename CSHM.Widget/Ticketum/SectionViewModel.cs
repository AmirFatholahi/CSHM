using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Ticketum
{
    public class SectionViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int DepartementID { get; set; }
        public bool IsActive { get; set; }
    }
}
