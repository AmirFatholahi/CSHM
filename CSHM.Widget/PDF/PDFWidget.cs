using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.PDF
{
    public class PDFWidget : IPDFWidget
    {
        public PDFWidget()
        {

        }

        public HttpResponseMessage GeneratePDF()
        {

            var file = $"D:\\Presentation2.pdf";
            var bytes = System.IO.File.ReadAllBytes(file);

            MemoryStream ms = new MemoryStream(bytes);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ms.ToArray())
            };
            // result.Content = new StreamContent(ms);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "myTest" + ".pdf"

            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //  result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms.excel");
            result.Content.Headers.ContentLength = bytes.Length;
            return result;
        }
    }
}
