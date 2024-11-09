using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Presentations.Media
{
    public class MediaFileViewModel
    {
        public MediaFileViewModel() { }

        public MediaViewModel Media { get; set; }

        public IFormFile? File { get; set; }

        public bool IsNeedToGrayscale { get; set; }

        public bool IsNeedToCompress { get; set; }
    }
}
