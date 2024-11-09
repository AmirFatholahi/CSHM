using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentation.Base
{
    public class CMSTokenViewModel
    {
        public CMSTokenViewModel()
        {
            Errors = new List<ErrorViewModel>();
        }
        public string Status { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public string ExpireDateTime { get; set; }

        public List<ErrorViewModel> Errors { get; set; }
    }
}
