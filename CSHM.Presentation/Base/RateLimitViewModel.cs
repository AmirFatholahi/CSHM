using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentation.Base
{
    public class RateLimitViewModel
    {
        public int SegmentsPerWindow { get; set; }

        public int PermitLimit { get; set; }

        public int QueueLimit { get; set; }

        public int WindowSecond { get; set; }
    }
}
