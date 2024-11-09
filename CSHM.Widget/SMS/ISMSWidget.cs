using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.SMS;

public interface ISMSWidget
{
    SMSOutputViewModel Send(string content);

    List<int> Send(List<string> contents);

}