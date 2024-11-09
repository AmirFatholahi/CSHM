using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentations.Media
{
    public class MediaViewModel
    {
        public int ID { get; set; }

        public string EntityName { get; set; }

        public int EntityID { get; set; }

        public int MediaTypeID { get; set; }

        public string MediaTypeTitle { get; set; }

        public int ExtensionTypeID { get; set; }

        public string ExtensionTypeTitle { get; set; }

        public string FileName { get; set; }

        public string DisplayName { get; set; }

        public int Sequence { get; set; }

        public string ExtensionName { get; set; }

        public string Url { get; set; }

        public long Size { get; set; }

        public bool IsDefault { get; set; }

        public bool IsConfirm { get; set; }

        public bool IsActive { get; set; }
    }
}
