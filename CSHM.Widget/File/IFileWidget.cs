using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.File
{
    public interface IFileWidget
    {
        bool CreateFolder(string path, string folderName);

        public long GetFileSize(System.IO.Stream stream);

        public long GetFileSize(Bitmap fileBitmap);

        public byte[] HashMD5File(FileStream fileStream);

        bool FileExtensionMatcher(System.IO.Stream original, List<ExtensionTypeViewModel> extensions);
    }
}
