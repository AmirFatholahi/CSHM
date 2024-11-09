using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Captcha
{
    public class CaptchaViewModel
    {
        public string CapthaWord { get; set; }

        public string SessionID { get; set; }

        public DateTime ExpireDateTime { get; set; }

    }
}
