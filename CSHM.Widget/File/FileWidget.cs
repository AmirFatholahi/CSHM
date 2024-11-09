using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CSHM.Widget.Log;
using CSHM.Widget.Method;

namespace CSHM.Widget.File
{
    public class FileWidget : IFileWidget
    {
        private readonly ILogWidget _log;
        public FileWidget(ILogWidget log)
        {
            _log = log;
        }


        /// <summary>
        /// چک کننده فرمت فایل بطور واقعی
        /// </summary>
        /// <param name="original"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public bool FileExtensionMatcher(System.IO.Stream original, List<ExtensionTypeViewModel> extensions)
        {
            FileTypeChecker fileTypeChecker = new FileTypeChecker();
            var fileType = fileTypeChecker.GetFileType(original, extensions);
            if (fileType != FileType.Unknown)
                return true;
            else
                return false;

        }

        /// <summary>
        /// ایجاد پوشه
        /// </summary>
        /// <param name="path">مسیر ایجاد</param>
        /// <param name="folderName">نام پوشه</param>
        /// <returns></returns>
        public bool CreateFolder(string path, string folderName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(folderName))
                {
                    string directory = path + "\\" + folderName;
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod().GetSourceName(), 0);
                return false;
            }


        }


        /// <summary>
        /// دریافت اندازه فایل
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public long GetFileSize(System.IO.Stream stream)
        {
            return stream.Length;
        }

        public long GetFileSize(Bitmap fileBitmap)
        {
            MemoryStream dest = new MemoryStream();
            fileBitmap.Save(dest, ImageFormat.Jpeg);
            return GetFileSize(dest);
        }

        /// <summary>
        /// ایجاد هش فایل
        /// </summary>
        /// <param name="fileStream">فایل دریافت شده از کاربر</param>
        /// <returns></returns>
        public byte[] HashMD5File(FileStream fileStream)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(fileStream);
            }
        }
    }
}
