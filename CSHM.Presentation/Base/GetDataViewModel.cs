using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentation.Base
{
    public class GetDataViewModel
    {
        public string EntityName1 { get; set; }

        public string EntityName2 { get; set; }

        public int EntityID1 { get; set; }

        public int EntityID2 { get; set; }

        public int EntityID3 { get; set; }

        public bool Status1 { get; set; }

        public bool Status2 { get; set; }

        public bool Status3 { get; set; }

        public string ApiKey { get; set; }

        public string Token { get; set; }

        public bool? Activate { get; set; }

        public int? PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Filter { get; set; }
    }
}
