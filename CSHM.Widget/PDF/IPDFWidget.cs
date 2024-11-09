using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.PDF
{
    public interface IPDFWidget
    {
        HttpResponseMessage GeneratePDF();
    }
}
