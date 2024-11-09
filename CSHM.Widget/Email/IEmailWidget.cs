using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Email;

public interface IEmailWidget
{
    bool Send(string to, string subject, string content, Dictionary<string, string> attachments, bool isBodyHtml);

    bool IsEmailValid(string email);
}