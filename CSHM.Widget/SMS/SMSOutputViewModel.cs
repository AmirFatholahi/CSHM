using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.SMS
{
    public class SMSOutputViewModel
    {
        public long MessageID { get; set; }

        public string Message { get; set; }

        public long TemplateID { get; set; }

        public bool IsSuccess { get; set; }
    }
}
