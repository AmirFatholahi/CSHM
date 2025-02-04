using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Core.Presentations.User
{
    public class UserLogViewModel
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public string Action { get; set; }

        public string Title { get; set; }

        public string MetaData { get; set; }

        public string EventDateTime { get; set; }

        public string UserIP { get; set; }

        public int TotalCount { get; set; }
    }
}
