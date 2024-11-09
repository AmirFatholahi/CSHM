using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Redis
{
    public class BroutForceViewModel
    {
        public string Username { get; set; }

        public int Count { get; set; }

        public bool IsLocked { get; set; }

        public DateTime EnableTime { get; set; }
    }
}
