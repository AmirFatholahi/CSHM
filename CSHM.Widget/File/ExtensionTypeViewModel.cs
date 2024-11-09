using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.File
{
    public class ExtensionTypeViewModel
    {
        public int ID { get; set; }

        public string ExtensionName { get; set; }

        public string? MatcherType { get; set; }

        public string Postfix { get; set; }

        public byte[]? Matcher { get; set; }

        public bool IsImage { get; set; }
    }
}
